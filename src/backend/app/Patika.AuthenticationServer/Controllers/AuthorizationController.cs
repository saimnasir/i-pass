using IPass.Application;
using IPass.Application.Contracts.AccountDomain;
using IPass.Domain.CommonDomain.Exceptions;
using IPass.Shared.Consts;
using IPass.Shared.DTO.Common;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Patika.IdentityServer.Shared.Dto; 
using Patika.Shared.DTO;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Enums;
using Patika.Shared.Exceptions;
using Patika.Shared.Extensions;
using Patika.Shared.Identity.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using ApplicationException = Patika.Shared.Exceptions.ApplicationException;

namespace Patika.AuthenticationServer.Controllers
{
	[ApiExplorerSettings(GroupName = "Authentication Server")]
    [Route("identity")]
    public class AuthorizationController : GenericApiController
    {

        private const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IEmailSender _emailSender;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        IIdentityApplicationService IdentityApplicationService { get; }
        IWrongPasswordAttemptRepository WrongPasswordAttemptRepository { get; }
        IUserRefreshTokenRepository UserRefreshTokenRepository { get; }

        public AuthorizationController(
            SignInManager<ApplicationUser> signInManager,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            Configuration configuration,
            IIdentityApplicationService identityApplicationService,
            ILogWriter logWriter,
            IWrongPasswordAttemptRepository wrongPasswordAttemptRepository,
            IUserRefreshTokenRepository userRefreshTokenRepository
            ) : base(logWriter, configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailSender = emailSender;
            IdentityApplicationService = identityApplicationService;
            WrongPasswordAttemptRepository = wrongPasswordAttemptRepository;
            UserRefreshTokenRepository = userRefreshTokenRepository;
            _emailStore = GetEmailStore();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("connect/logout")]
        public async Task<IActionResult> LogoutPost()
        {
            await _signInManager.SignOutAsync();
            return SignOut(
                authenticationSchemes: AuthenticationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = "/"
                });
        }


        [Produces("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterServiceman(UserRegistrationInputDto input)
        {
            return await WithLogging(input, async (input) =>
            {
                var registrationResponse = await IdentityApplicationService.RegisterUserAsync(input);
                ApplicationUser user = await _userManager.FindByEmailAsync(registrationResponse.Email);
                await IdentityApplicationService.AddRoleToUserAsync(user, Consts.USER_ROLE);
                var tokenResult = await MakeTokenAsync(user);
                tokenResult.IsActivationCodeSent = registrationResponse.IsActivationCodeSent;
                await SetUserRefreshToken(user, tokenResult);
                return tokenResult;
            });
        }
         


        [Produces("application/json")]
        [HttpPost("connect/token")]
        public async Task<IActionResult> Exchange(BasicLoginInputDto input)
        {
            var user = await IdentityApplicationService.GetByPhoneNumberAsync(input.PhoneNumber);
            
            if(user is null)
                return Unauthorized("Kullanıcı bulunamadı");

            if (await WrongPasswordAttemptRepository.IsUserBlockedAsync(user.Id))
                return Unauthorized("Hesabına fazla sayıda yanlış giriş denemesi yapıldığından dolayı güvenliğin için hesabını kilitledik. Hesabına tekrar giriş yapabilmek için <b>Şifremi Unuttum</b> ile yeni bir şifre belirlemen yeterli.");

            if (await _userManager.CheckPasswordAsync(user, input.Password))
            {
                var res = await MakeTokenAsync(user);

                user = await _userManager.FindByIdAsync(user.Id);

                await SetUserRefreshToken(user, res);

                return Ok(res);
            }
            else
            {
                await WrongPasswordAttemptRepository.InsertOneAsync(new WrongPasswordAttempt
                {
                    AttemptTime = DateTime.Now,
                    UserId = user.Id
                });
            }
            return Unauthorized("Kullanıcı şifresi hatalı");
        }

        private Task SetUserRefreshToken(ApplicationUser user, TokenResultDto res) => SetUserRefreshToken(UserRefreshTokenRepository, user, res, Configuration);

        public static async Task SetUserRefreshToken(IUserRefreshTokenRepository userRefreshTokenRepository, ApplicationUser user, TokenResultDto res, Configuration configuration)
        {
            await userRefreshTokenRepository.InsertOneAsync(new UserRefreshToken
            {
                RefreshToken = res.RefreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(configuration.JWT.RefreshTokenValidityInDays),
                Token = res.Token,
                UserId = user.Id
            });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenInputDto tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var claims = GetPrincipalFromExpiredToken(accessToken);
            if (claims == null || !claims.Any())
            {
                return BadRequest("Invalid access token or refresh token");
            }

            #pragma warning disable CS8600
            #pragma warning disable CS8602
            string userId = claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
            #pragma warning restore CS8602
            #pragma warning restore CS8600

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return BadRequest("Invalid access token or refresh token");

            var token = await UserRefreshTokenRepository.GetRefreshTokenAsync(userId, tokenModel.RefreshToken);

            if (token == null || token.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(claims.ToList(), Configuration);
            var newRefreshToken = GenerateRefreshToken();

            await UserRefreshTokenRepository.InsertOneAsync(new UserRefreshToken
            {
                RefreshToken = newRefreshToken,
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(Configuration.JWT.RefreshTokenValidityInDays),
                UserId = userId
            });

            return new ObjectResult(new TokenResultDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                ExpiresIn = (long)newAccessToken.ValidTo.Subtract(DateTime.Now).TotalSeconds,
                IsActivationCodeSent = user.IsActivationCodeValidated,
                IsActivationCodeValidated = user.IsActivationCodeValidated,
                IsProfileCompleted = user.IsProfileCompleted,
                TokenType = ""
            });
        }

        private IActionResult ForbiddenResult(string desc,string error = "Invalid grnat type") => 
            Forbid(
					authenticationSchemes: AuthenticationScheme,
					properties: new AuthenticationProperties(new Dictionary<string, string?>
					{
						["Error"] = error,
						["Description"] = desc
					}));		

        [HttpGet("authorize/google-login")]
        public IActionResult LoginWithGoogleAsync(string callback)
        {
            if (string.IsNullOrEmpty(callback))
            {
                return ToGeneralResponse(Guid.Empty.ToString(), new ApplicationException("Identity:001", "Callback gerekli."));
                //return ForbiddenResult("Forbidden", Errors.LoginRequired);
            }
            var provider = "Google";
            var url = $"/identity/authorize/callback?callback={callback}";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, url);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet("authorize/facebook-login")]
        public IActionResult LoginWithFacebookAsync(string callback)
        {
            if (string.IsNullOrEmpty(callback))
            {
                return ToGeneralResponse(Guid.Empty.ToString(), new ApplicationException("Identity:001", "Callback gerekli."));
                //return ForbiddenResult("Forbidden", Errors.LoginRequired);
            }
            var provider = "Facebook";
            var url = $"/identity/authorize/callback?callback={callback}";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, url);
            return new ChallengeResult(provider, properties);
        }

        [Produces("application/json")]
        [HttpGet("authorize/callback")]
        public async Task<IActionResult> CallBack(string? remoteError = null, string? callback = null)
        {            
            return await ProcessCallBack(remoteError ?? "", callback ?? "", role: Consts.USER_ROLE);
        }

        private async Task<IActionResult> ProcessCallBack(string remoteError, string callback, string role)
        {
            if (!string.IsNullOrEmpty(remoteError))
            {
                return Json(GeneralResponseDTO.ErrorResponse($"Error from external provider: {remoteError}"));
            }
            if (string.IsNullOrEmpty(callback))
            {
                return ToGeneralResponse(Guid.Empty.ToString(), new ApplicationException("Identity:001", "Callback gerekli."));
                //return ForbiddenResult("Forbidden", Errors.LoginRequired);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Json(GeneralResponseDTO.ErrorResponse("Error loading external login information."));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if(await _userManager.IsInRoleAsync(user, role))
                {
                    var tokenResult = await MakeTokenAsync(user);
                    await SetUserRefreshToken(user, tokenResult);
                    return Redirect($"{callback}&t={tokenResult.Token}&e={tokenResult.ExpiresIn}&acs={tokenResult.IsActivationCodeSent}&acv={tokenResult.IsActivationCodeValidated}&pc={tokenResult.IsProfileCompleted}&r={tokenResult.RefreshToken}");
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    var ex = new UserRoleDoesNotMatchException();
                    return Json(new GeneralResponseDTO<object>
                    {
                        Message = ex.Message,
                        Result = false,
                        ResultCode = LogStatus.InternalError,
                        Exception = new ApplicationException(ex)
                    });
                }
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    var res = await CreateUserAsync(role);
                    if (res.ResultCode != LogStatus.Success)
                        return Json(res);
                    user = await _userManager.FindByEmailAsync(email);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, new UserLoginInfo(
                        info.LoginProvider, info.ProviderKey, info.ProviderDisplayName));
                }
                result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (result.Succeeded)
                {
                    var tokenResult = await MakeTokenAsync(user);
                    await SetUserRefreshToken(user, tokenResult);
                    return Redirect($"{callback}&t={tokenResult.Token}&e={tokenResult.ExpiresIn}&acs={tokenResult.IsActivationCodeSent}&acv={tokenResult.IsActivationCodeValidated}&pc={tokenResult.IsProfileCompleted}&r={tokenResult.RefreshToken}");
                }
                return Json(GeneralResponseDTO.ErrorResponse("Unknown error occured"));
            }
        }

        [HttpGet("application-user")]
        [Authorize(AuthenticationSchemes = AuthenticationScheme)]
        public async Task<IActionResult> GetApplicationUser()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            var userDto = IdentityApplicationService.ApplicationUserMapToDtoAsync(user);
            userDto.Roles = User.Claims.Where(m=>m.Type == ClaimTypes.Role).Select(m => m.Value).ToList();
            return Json(userDto);
        }

		[HttpGet("token-expires-in")]
		[Authorize(AuthenticationSchemes = AuthenticationScheme)]
		public Task<IActionResult> GetTokenExpiresIn() => Task.FromResult(Content(User.FindFirstValue("exp")) as IActionResult);

		[HttpGet("application-user-by-phone")]
        [Authorize(AuthenticationSchemes = AuthenticationScheme)]
        public async Task<IActionResult> GetApplicationUserByPhone(string phoneNumber)
        {
            var user = await IdentityApplicationService.GetByPhoneNumberAsync(phoneNumber);
            var userDto = IdentityApplicationService.ApplicationUserMapToDtoAsync(user);
            return Json(userDto);
        }

        [HttpGet("token")]
        [Authorize(AuthenticationSchemes = AuthenticationScheme)]
        public async Task<IActionResult> GetTokenAsync()
        {
            var user = await _userManager.FindByEmailAsync(HttpContext.User.FindFirstValue(ClaimTypes.Email));
            var tokenResult = await MakeTokenAsync(user);
            return Json(tokenResult);
        }

		private Task<TokenResultDto> MakeTokenAsync(ApplicationUser user) => MakeTokenAsync(user, _userManager, Configuration);

		[HttpPost("reset-forgotten-password")]
        public async Task<IActionResult> ResetForgottenPassword(ResetPasswordInputDto input)
		{
            return await WithLogging(input,
                async (i) =>
                {
                    await IdentityApplicationService.ResetPassword(i);
                });
		}

		private async Task<IActionResult> WithLogging<T>(T input, Func<T,Task> tryPart) where T : DTO
		{
			try
			{
                await tryPart(input);
                return Json(new { Message = "", Result = true, ResultCode = $"{LogStatus.Success}", JobId = input.LogId });
            }
            catch (BaseApplicationException ex)
            {
                return ToGeneralResponse(input.LogId, ex);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new
                    {
                        code = new GeneralException(ex).Code,
                        message = ex.Message,
                        innerException = ex.InnerException?.Message ?? "",
                        source = ex.Source
                    },
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId,
                    result = new { }
                });
            }
        }

        private JsonResult ToGeneralResponse(string jobLogId, BaseApplicationException ex)
        {
            return Json(new
            {
                resultCode = $"{LogStatus.Exception}",
                exception = new
                {
                    code = ex.Code,
                    message = ex.Message,
                    innerException = ex.InnerException?.Message ?? "",
                    source = ex.Source
                },
                done = false,
                message = ex.Message,
                jobId = jobLogId,
                result = new { }
            });
        }

        private async Task<IActionResult> WithLogging<T,U>(T input, Func<T, Task<U>> tryPart) where T : DTO
        {
            try
            {
                var res = await tryPart(input);
                return Json(new { Message = "", Result = res, ResultCode = $"{LogStatus.Success}", JobId = input.LogId });
            }
            catch (BaseApplicationException ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new ApplicationException(ex),
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId,
                    result = new { }
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    resultCode = $"{LogStatus.Exception}",
                    exception = new GeneralException(ex),
                    done = false,
                    message = ex.Message,
                    jobId = input.LogId,
                    result = new { }
                });
            }
        }

        private async Task<GeneralResponseDTO<object>> CreateUserAsync(string role)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            if (info == null)
            {
                return GeneralResponseDTO<object>.ErrorResponse("Error loading external login information during confirmation.");
            }

            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, email, CancellationToken.None);

            user.FirstName = name;
            user.LastName = lastName ?? string.Empty;
            user.Id = new Guid().NewSequentalGuid().ToString();

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId, code },
                        protocol: Request.Scheme) ?? "";

                    await _emailSender.SendEmailAsync(email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // If account confirmation is required, we need to show the link if we don't have a real email sender
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        throw new Exception();
                    }

                    await _userManager.AddToRoleAsync(user, role);

                    await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                    //return LocalRedirect(returnUrl);
                }
            }
            var str = "";
            foreach (var error in result.Errors)
            {
                str += $"\r\n {error.Description}";
            }
            if (str.Length > 0)
                return GeneralResponseDTO<object>.ErrorResponse(str);
            return GeneralResponseDTO<object>.SuccessResponse();
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        private static JwtSecurityToken CreateToken(List<Claim> authClaims, Configuration configuration)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWT.Secret));
            var dt = DateTime.Now.AddMinutes(configuration.JWT.TokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: configuration.JWT.ValidIssuer,
                audience: configuration.JWT.ValidAudience,
                expires: dt,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private IEnumerable<Claim> GetPrincipalFromExpiredToken(string? token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken.Claims;
        }

        public static async Task<TokenResultDto> MakeTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager, Configuration configuration)
        {
            JwtSecurityToken token;
			string refreshToken;
			(token, refreshToken) = await GenerateTokenAndRefreshTokenAsync(user, userManager, configuration);
            var obj = new TokenResultDto
            {
                ExpiresIn = (long)token.ValidTo.Subtract(DateTime.Now).TotalSeconds,
                RefreshToken = refreshToken,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                IsActivationCodeSent = user.IsActivationCodeValidated,
                IsActivationCodeValidated = user.IsActivationCodeValidated,
                IsProfileCompleted = user.IsProfileCompleted,
                TokenType = ""
            };

            obj.IsProfileCompleted = user?.IsProfileCompleted ?? false;
            obj.IsActivationCodeValidated = user?.IsActivationCodeValidated ?? false;

            return obj ?? throw new Exception();
        }

        private static async Task<Tuple<JwtSecurityToken, string>> GenerateTokenAndRefreshTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager, Configuration configuration)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return new Tuple<JwtSecurityToken, string>(CreateToken(authClaims, configuration), GenerateRefreshToken());
        }
    }

}
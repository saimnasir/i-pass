using IPass.Application.Contracts.AccountDomain;
using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Shared.Consts;
using IPass.Shared.DTO.Common;
using IPass.Shared.DTO.Identity;
using IPass.Shared.Extensions;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Patika.Application;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Exceptions.AccountDomain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPass.Application.AccountDomain
{
    public class IdentityApplicationService : ApplicationService, IIdentityApplicationService
    {
        UserManager<ApplicationUser> UserManager { get; }
        IUserStore<ApplicationUser> UserStore { get; }
        SignInManager<ApplicationUser> SignInManager { get; }
        IIdentityServiceValidators AccountServiceValidators { get; }
        IdentityDbContext<ApplicationUser> IdentityDbContext { get; }
        IUserEmailStore<ApplicationUser> EmailStore { get; set; }

        public IdentityApplicationService(
            UserManager<ApplicationUser> userManager,
            Configuration configuration,
            ILogWriter LogWriter,
            ClientAuthenticationParams authenticationParams,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServiceValidators accountServiceValidators,
            IdentityDbContext<ApplicationUser> identityDbContext
            ) : base(configuration, new GeneralMappingProfile(), LogWriter)
        {
            UserManager = userManager;
            UserStore = userStore;
            SignInManager = signInManager;
            AccountServiceValidators = accountServiceValidators;
            IdentityDbContext = identityDbContext;
            EmailStore = GetEmailStore();
        }

        public async Task<SendActivationCodeOutputResponse> SendAccountValidationSmsAsync(SendActivationCodeInputDto input)
        {
            try
            {
                var id = IdentityDbContext.Users.AsNoTracking().SingleOrDefault(m => m.PhoneNumber == input.PhoneNumber).Id;
                ApplicationUser = await UserManager.FindByIdAsync(id);
                if (ApplicationUser == null)
                    throw new UserNotFoundException();
                await LogWriter.AddCodeMileStoneLogAsync(input, "Send Account Validation Sms Started", GetType(), input);
                await LogWriter.AddCodeMileStoneLogAsync(input, "User Account Activation Validator Processing", GetType());

                await LogWriter.AddCodeMileStoneLogAsync(input, "User Account Activation Validator Processed", GetType());

                ApplicationUser.PhoneNumber = input.PhoneNumber;

                await LogWriter.AddCodeMileStoneLogAsync(input, "SendActivationCodeSmsAsync start", GetType());
                 
                await LogWriter.AddCodeMileStoneLogAsync(input, "SendActivationCodeSmsAsync ends", GetType());

                ApplicationUser.ActivationCode = string.Empty;
                ApplicationUser.ActivationCodeExpireDate = GetActivationCodeExpireDate();
                ApplicationUser.ActivationCodeTryCount = 0;
                ApplicationUser.IsActivationCodeValidated = false;
                ApplicationUser.PhoneNumberConfirmed = false;

                await UserManager.UpdateAsync(ApplicationUser);

                await LogWriter.AddCodeMileStoneLogAsync(input, "Account Validation Sms Sent", GetType());

                return new SendActivationCodeOutputResponse
                {
                    LogId = input.LogId
                };
            }
            catch (Exception ex)
            {
                await LogWriter.AddExceptionLogAsync(input, ex, GetType());
                throw;
            }
        }

        public async Task<ValidateAccountOutputResponse> ValidateAccountAsync(ValidateAccountInputDto input)
        {
            var id = IdentityDbContext.Users.AsNoTracking().SingleOrDefault(m => m.UserName == input.UserName).Id;
            ApplicationUser = await UserManager.FindByIdAsync(id);
            if (ApplicationUser == null)
                throw new UserNotFoundException();

            ApplicationUser.ActivationCode = input.ActivationCode;
            await UserManager.UpdateAsync(ApplicationUser);

            await UserManager.UpdateAsync(ApplicationUser);

            await AddRoleToUserAsync(ApplicationUser, Consts.USER_VALIDATED_ROLE);

            return new ValidateAccountOutputResponse
            {
                IsSuccess = true,
                LogId = input.LogId
            };
        }

        //public async Task UpdateUserProfileAsync(UpdateUserProfileInputDto input, string token)
        //{
        //    var id = (await GetApplicationUser(token)).Id;
        //    ApplicationUser = await UserManager.FindByIdAsync(id);
        //    if (ApplicationUser == null)
        //        throw new UserNotFoundException();
        //    await AccountServiceValidators.ValidateUpdateUserProfileInputAsync(input, ApplicationUser);
        //    // ASK : Validasyona gerek var mı? WebApp tarafında zaten validasyon yapılıyor.
        //    if (ApplicationUser.Email != input.Email)
        //    {
        //        if ((await UserManager.FindByEmailAsync(input.Email)) is not null)
        //        {
        //            throw new Exception("Email kullanılmaktadır.");
        //        }
        //    }
        //    ApplicationUser.FirstName = input.FirstName;
        //    ApplicationUser.LastName = input.LastName;
        //    ApplicationUser.Email = input.Email;
        //    ApplicationUser.IsProfileCompleted = true;
        //    //ApplicationUser.PhoneNumberConfirmed = true;
        //    // ASK : Bunlar burda mı olacak.
        //    var res = await UserManager.UpdateAsync(ApplicationUser);

        //    if (!res.Succeeded)
        //    {
        //        throw new Exception(res.Errors.First().Description);
        //    }
        //}

        public async Task<UserRegistrationOutputResponse> RegisterUserAsync(UserRegistrationInputDto input)
        {
            await LogWriter.AddCodeMileStoneLogAsync(input, "Register User Started", GetType(), input);
            await LogWriter.AddCodeMileStoneLogAsync(input, "Register User Input Validating", GetType());

            await AccountServiceValidators.ValidateRegistrationInputAsync(input);
            await LogWriter.AddCodeMileStoneLogAsync(input, "Register User Input Validated", GetType());

            input.Email = input.Email.Trim();

            var user = CreateUser();
            await UserStore.SetUserNameAsync(user, input.UserName, CancellationToken.None);
            await EmailStore.SetEmailAsync(user, input.Email, CancellationToken.None);

            var result = await UserManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                await SignInManager.PasswordSignInAsync(user, input.Password, isPersistent: false, lockoutOnFailure: true);
                await SetApplicationUserAsync(user);
                return new UserRegistrationOutputResponse
                {
                    Email = input.Email,
                    IsActivationCodeSent = false,
                    LogId = input.LogId,
                };
            }
            throw new RegisterUserFailedException();
        }

        public async Task<string> RegisterApplicationAsync(ApplicationRegistrationInputDto input)
        {
            var user = CreateUser();
            await UserStore.SetUserNameAsync(user, input.ApplicationName, CancellationToken.None);
            await EmailStore.SetEmailAsync(user, $"{input.ApplicationName}@patika.com", CancellationToken.None);
            user.UserName = input.ApplicationName;
            var result = await UserManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
                return user.Id;

            throw new RegisterUserFailedException();
        }

        private DateTime GetActivationCodeExpireDate()
        {
            var seconds = Configuration.AccountConfig.ActivationCodeExpireInSeconds;
            return DateTime.Now.AddSeconds(seconds);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!UserManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)UserStore;
        }


        public ApplicationUserDto ApplicationUserMapToDtoAsync(ApplicationUser user)
        {
            // ASK: To Team? How to use mapper on controller
            return Mapper.Map<ApplicationUserDto>(user);
        }

        public async Task ResetPassword(ResetPasswordInputDto input)
        {
            var user = await IdentityDbContext.Users.FirstOrDefaultAsync(m => m.UserName == input.UserName);
            user = await UserManager.FindByIdAsync(user.Id);
            var token = await UserManager.GeneratePasswordResetTokenAsync(user);

            var res = await UserManager.ResetPasswordAsync(user, token, input.NewPassword);
            if (res == null || !res.Succeeded)
            {
                var error = res.Errors.First();
                await LogWriter.AddCodeMileStoneLogAsync(input, $"Reset password error:{error.Code} -> {error.Description}", GetType());
                throw new PasswordFormatInvalidException();
            }
        }

        public async Task<ApplicationUser> GetByUserNameAsync(string userName)
            => await IdentityDbContext.Users.FirstOrDefaultAsync(m => m.UserName == userName);

        public async Task AddRoleToUserAsync(ApplicationUser user, params string[] roles)
        {
            foreach (var role in roles)
            {
                await UserManager.AddToRoleAsync(user, role);
            }
        }
    }
}

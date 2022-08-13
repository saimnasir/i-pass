using IPass.Application;
using IPass.Application.AccountDomain;
using IPass.Application.AccountDomain.Validators;
using IPass.Application.CommonDomain.Validators;
using IPass.Application.Contracts.AccountDomain;
using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using IPass.Domain.PasswordDomain.Repositories;
using IPass.EFRepositories.IPassContext;
using IPass.EFRepositories.IPassContext.Repositories;
using IPass.EFRepositories.Services;
using IPass.Shared.Extensions;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Patika.Application.Contracts;
using Patika.IdentityServer.Shared;
using Patika.IdentityServer.Shared.IdentityContext.Repositories;
using Patika.IdentityServer.Shared.Repositories;
using Patika.Shared.Consts;
using Patika.Shared.Entities;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Events;
using Patika.Shared.Extensions;
using Patika.Shared.Identity.Repositories;
using Patika.Shared.Interfaces;
using Patika.Shared.Services;
using Quartz;
using System.Text;

namespace Patika.AuthenticationServer
{
    internal class Startup
    {
        public static ClientAuthenticationParams AuthenticationParams { get; private set; } = new ClientAuthenticationParams();

        public IConfiguration Configuration { get; }
        public Configuration AppConfiguration { get; set; }
        public IWebHostEnvironment Environment { get; }
        public static Uri PublicGateWayUri { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            AddEmailSender(services);
            AddConfiguration(services);

            AddQuartz(services);

            AddAuthentication(services);

            AddSwagger(services);

            AddApplicationServices(services);

            services.AddDbContext<MyMemoryDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(sp.GetService<Configuration>()?.RDBMSConnectionStrings.Single(m => m.Name.Equals(DbConnectionNames.Main)).FullConnectionString ?? "");
            }, ServiceLifetime.Scoped);

            services.AddDbContext<IdentityServerDbContext>(options => options.UseSqlServer(
                Configuration.GetSection("ConnectionStrings:DefaultConnection").Value
                ), ServiceLifetime.Scoped);

            AddRepositories(services);
            services.AddScoped<IMigrationStep, DefaultDataMigration>();

            ConfigureAuthenticationParams(services);
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            AddValidators(services);
            SetupCORS(services);
        }
        private static void SetupCORS(IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy("corsapp", policy =>
                {
                    policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    ;
                });
            });
        }

        private void AddConfiguration(IServiceCollection services)
        {
            var config = new Configuration();
            Configuration.GetSection(nameof(Configuration)).Bind(config);
            services.AddSingleton(config);
            AppConfiguration = new Configuration();
            Configuration.GetSection("Configuration").Bind(AppConfiguration);
            LogWriterExtensions.ApplicationName = config.ApplicationName;
        }


        private void AddEmailSender(IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, NullEmailSender>();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var jwtSettings = AppConfiguration.JWT;
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<IdentityServerDbContext>()
              .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();

            var auth = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = jwtSettings.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = AppConfiguration.JWT.ValidAudience,
                    ValidIssuer = AppConfiguration.JWT.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWT.Secret)),
                    RequireExpirationTime = AppConfiguration.JWT.RequireExpirationTime
                };
            });

            auth.AddGoogle(options =>
            {
                options.ClientId = "932188321669-qqq478ic84qe2k6rijvp79ph177h51vp.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                options.Scope.Add("email");
            });

            auth.AddFacebook(options =>
            {
                options.ClientId = "5621050854581272";
                options.AppSecret = "bfd0a2f9893cc300b9e6ff8b132f5385";
                options.Scope.Add("public_profile");
                options.Scope.Add("email");
            });

            AddOpenIdDict(services);
        }

        private void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IdentityServerDbContext>();
            services.AddScoped<IdentityDbContext<ApplicationUser>, IdentityServerDbContext>();

            services.AddScoped<IIdentityApplicationService, IdentityApplicationService>();
            services.AddScoped<MappingProfile, GeneralMappingProfile>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogWriter, LogWriter>();

            services.AddScoped<IUnitOfWorkHostWithInterface, MyMemoryDbContext>();

            //services.AddScoped<IAppConfigRepository, AppConfigRepository>();

            //services.AddScoped<ISwearWordRepository, SwearWordRepository>();
            //services.AddScoped<IInvalidNameCombinationRepository, InvalidNameCombinationRepository>();

            services.AddScoped<IWrongPasswordAttemptRepository, WrongPasswordAttemptRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        }

        private void AddValidators(IServiceCollection services)
        {
            // services.AddFluentValidation();

            services.AddScoped<IIdentityServiceValidators, IdentityServiceValidators>();

            services.AddScoped<IPhoneNumberValidator, PhoneNumberValidator>();
            services.AddScoped<IUserNameValidator, UserNameValidator>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IRegexValidator, RegexValidator>();
            services.AddScoped<IPhoneNumberExistanceValidator, PhoneNumberExistanceValidator>();
            services.AddScoped<IUserNameExistanceValidator, UserNameExistanceValidator>();
            services.AddScoped<IEmailExistanceValidator, EmailExistanceValidator>();
            services.AddScoped<IValidateActivationCodeValidator, ValidateActivationCodeValidator>();
            services.AddScoped<ISendActivationCodeSmsValidator, SendActivationCodeSmsValidator>();
            services.AddScoped<IEmailValidator, EmailValidator>();
            services.AddScoped<IFirstNameValidator, FirstNameValidator>();
            services.AddScoped<ILastNameValidator, LastNameValidator>();
            services.AddScoped<IProfileAlreadyCompletedValidator, ProfileAlreadyCompletedValidator>();
        }
        public void Configure(IApplicationBuilder app)
        {
            Migrate(app);

            app.UseForwardedHeaders();

            PublicGateWayUri = new Uri(AppConfiguration.GatewayUrl);

            ConfigurationEvents.NewConfiguration(app.ApplicationServices.GetRequiredService<Configuration>()); // ASK : Validation için repository gerekli (redis)


            if (Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server API V1");
                    c.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (ctx, next) =>
            {
                ctx.Request.Scheme = PublicGateWayUri.Scheme;

                ctx.Request.Host = new HostString(PublicGateWayUri.Authority);

                await next();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            //if (Environment.IsEnvironment("AzureStaging"))
            //{
                app.UseCors("corsapp");
            //}

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
                builder.MapRazorPages();
                builder.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void Migrate(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var migrator = services.GetRequiredService<IMigrationStep>();
                migrator.EnsureMigrationAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        private void AddSwagger(IServiceCollection services)
        {
            //if (!Environment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Identity Server's API Documentation",
                        Description = "Identity Server API"
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "Example: \"Bearer {token}\"",
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.DocInclusionPredicate((name, api) => true);
                    c.TagActionsBy(api =>
                    {
                        if (api.GroupName != null)
                        {
                            return new[] { api.GroupName };
                        }

                        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                        {
                            return new[] { controllerActionDescriptor.ControllerName };
                        }

                        throw new InvalidOperationException("Unable to determine tag for endpoint.");
                    });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                });
            }
        }

        private void AddOpenIdDict(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                  Consts.ADMIN_POLICY,
                  policy => policy
                    .RequireAuthenticatedUser()
                    .RequireRole(Consts.ADMINISTRATOR_ROLE)
                );
                AddOtherPolicies(options);
            });
        }

        private void AddOtherPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(
                  IPass.Shared.Consts.Consts.USER_POLICY,
                  policy => policy
                    .RequireAuthenticatedUser()
                    .RequireRole(IPass.Shared.Consts.Consts.USER_ROLE)
                );
        }

        private void ConfigureAuthenticationParams(IServiceCollection services)
        {
            AuthenticationParams = new ClientAuthenticationParams
            {
                AuthServer = Configuration.GetSection("Configuration:AuthServerUrl").Value,
                ClientId = Configuration.GetSection("Configuration:ClientId").Value,
                ClientSecret = Configuration.GetSection("Configuration:ClientSecret").Value
            };
            services.AddSingleton(AuthenticationParams);
        }

        private void AddQuartz(IServiceCollection services)
        {
            if (!Environment.IsDevelopment())
            {
                services.AddQuartz(options =>
                {
                    options.UseMicrosoftDependencyInjectionJobFactory();
                    options.UseSimpleTypeLoader();
                    options.UseInMemoryStore();
                });
                services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
            }
        }
    }
}

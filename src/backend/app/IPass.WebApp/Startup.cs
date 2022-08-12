using IPass.Application;
using IPass.Application.AccountDomain;
using IPass.Application.AccountDomain.Validators;
using IPass.Application.CommonDomain.Validators;
using IPass.Application.Contracts.AccountDomain;
using IPass.Application.Contracts.AccountDomain.Validators;
using IPass.Application.Contracts.CommonDomain.Validators;
using IPass.Application.Contracts.PasswordDomain;
using IPass.Application.Contracts.Services;
using IPass.Application.PasswordDomain;
using IPass.Domain.CommonDomain.Repositories;
using IPass.Domain.PasswordDomain.Repositories;
using IPass.EFRepositories.IPassContext;
using IPass.EFRepositories.IPassContext.Repositories;
using IPass.EFRepositories.Services;
using IPass.Shared.Extensions;
using IPass.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Patika.Application.Contracts;
using Patika.EF.Shared;
using Patika.Shared.Consts;
using Patika.Shared.Entities;
using Patika.Shared.Events;
using Patika.Shared.Interfaces;
using System;
using System.Linq;
using Configuration = Patika.Shared.Entities.Configuration;

namespace IPass.WebApp
{
    public class Startup
    {
        public static ClientAuthenticationParams AuthenticationParams { get; private set; } = new ClientAuthenticationParams();
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc();
            AddServices(services);
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

        private void AddServices(IServiceCollection services)
        {
            services
              .AddControllersWithViews()
              .AddNewtonsoftJson(options =>
                   options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            AddSwagger(services);

            AddConfiguration(services);

            services.AddDbContext<MyMemoryDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(sp.GetService<Configuration>().RDBMSConnectionStrings.Single(m => m.Name.Equals(DbConnectionNames.Main)).FullConnectionString);
            }, ServiceLifetime.Scoped);

            AddRepositories(services);
            AddApplicationServices(services);
            AddValidators(services);
            ConfigureOpenIdServer(services);
        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGenNewtonsoftSupport();

            //if (!Environment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
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
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "IPass Web Service API",
                        Description = "Main App Web Server API"
                        //TermsOfService = new Uri("https://example.com/terms"),
                        //Contact = new OpenApiContact
                        //{
                        //    Name = "Example Contact",
                        //    Url = new Uri("https://example.com/contact")
                        //},
                        //License = new OpenApiLicense
                        //{
                        //    Name = "Example License",
                        //    Url = new Uri("https://example.com/license")
                        //}
                    });

                    // using System.Reflection;
                    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
            }
        }

        private void ConfigureOpenIdServer(IServiceCollection services)
        {
            var jwtConfiguration = new JWTConfiguration();
            Configuration.Bind("Configuration:JWT", jwtConfiguration);
            AuthenticationParams = new ClientAuthenticationParams
            {
                AuthServer = Configuration.GetSection("Configuration:AuthServerUrl").Value,
                ClientId = Configuration.GetSection("Configuration:ClientId").Value,
                ClientSecret = Configuration.GetSection("Configuration:ClientSecret").Value
            };
            services.AddSingleton(AuthenticationParams);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = jwtConfiguration.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidateAudience = jwtConfiguration.ValidateAudience,
                    ValidateLifetime = jwtConfiguration.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtConfiguration.ValidateIssuerSigningKey,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = jwtConfiguration.ValidAudience,
                    ValidateIssuer = jwtConfiguration.ValidateIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
                    RequireExpirationTime = jwtConfiguration.RequireExpirationTime,
                };
            });

            AddAuthorization(services);

            services.AddControllers();
        }

        private void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogWriter, LogWriter>();

            services.AddScoped<IUnitOfWorkHostWithInterface, MyMemoryDbContext>();

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationTypeRepository, OrganizationTypeRepository>();
            services.AddScoped<IEnvironmentTypeRepository, EnvironmentTypeRepository>();
            services.AddScoped<IMemoryTypeRepository, MemoryTypeRepository>();
            services.AddScoped<IMemoryRepository, MemoryRepository>();
            services.AddScoped<IPinCodeRepository, PinCodeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<MappingProfile, GeneralMappingProfile>(); 
            services.AddScoped<IAccountApplicationService, AccountApplicationService>();

            services.AddScoped<IOrganizationAppService, OrganizationAppService>();
            services.AddScoped<IOrganizationTypeAppService, OrganizationTypeAppService>();
            services.AddScoped<IEnvironmentTypeAppService, EnvironmentTypeAppService>();
            services.AddScoped<IMemoryTypeAppService, MemoryTypeAppService>();
            services.AddScoped<IMemoryAppService, MemoryAppService>();
            services.AddScoped<IProfileAppService, ProfileAppService>();
        }

        private void AddValidators(IServiceCollection services)
        {

            services.AddScoped<IAccountApplicationServiceValidator, AccountApplicationServiceValidator>();

            services.AddScoped<IAccountValidationValidator, AccountValidationValidator>();
            services.AddScoped<IPhoneNumberExistanceValidator, PhoneNumberExistanceValidatorOverHttp>();
            services.AddScoped<IUserNameExistanceValidator, UserNameExistanceValidatorOverHttp>();
            services.AddScoped<IPhoneNumberValidator, PhoneNumberValidator>();
            services.AddScoped<IUserNameValidator, UserNameValidator>();
            services.AddScoped<IValidateActivationCodeValidator, ValidateActivationCodeValidator>();
            services.AddScoped<ISendActivationCodeSmsValidator, SendActivationCodeSmsValidator>();
            services.AddScoped<IGuidValidator, GuidValidator>();
            services.AddScoped<IRegexValidator, RegexValidator>();

            services.AddScoped<IBirthDateValidator, BirthDateValidator>();
            services.AddScoped<IEmailValidator, EmailValidator>();
            services.AddScoped<IFirstNameValidator, FirstNameValidator>();
            services.AddScoped<ILastNameValidator, LastNameValidator>();
        }
        private void AddConfiguration(IServiceCollection services)
        {
            var config = new Configuration();
            Configuration.GetSection(nameof(Configuration)).Bind(config);
            services.AddSingleton(config);
            LogWriterExtensions.ApplicationName = config.ApplicationName;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            Initiate(app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IPass API");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.Full);
            });
            app.UseRouting();

            app.UseCors("corsapp");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }

        private static void Initiate(IApplicationBuilder app)
        {
            Configuration configuration = app.ApplicationServices.GetRequiredService<Configuration>();
            ConfigurationEvents.NewConfiguration(configuration);
            if (configuration.AutoMigrate)
            {
                var ctx = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MyMemoryDbContext>();
                ctx.Database.Migrate();
            }
        }
    }
}

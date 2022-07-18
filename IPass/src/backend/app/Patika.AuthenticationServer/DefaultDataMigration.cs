using IPass.Application.Contracts.AccountDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Patika.IdentityServer.Shared;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities.Identity;
using Patika.Shared.Interfaces;

namespace Patika.AuthenticationServer
{
	internal class DefaultDataMigration : IMigrationStep
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private IIdentityApplicationService IdentityApplicationService { get; }

        public DefaultDataMigration(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            IdentityApplicationService = serviceProvider.GetService<IIdentityApplicationService>() ?? throw new Exception();
            _userManager = serviceProvider.GetService<UserManager<ApplicationUser>>() ?? throw new Exception();

        }
        public async Task EnsureMigrationAsync()
        {
            try
            {
                var context = _serviceProvider.GetRequiredService<IdentityServerDbContext>();
                await context.Database.MigrateAsync();

                await CreateDefaultRolesAsync(_serviceProvider);

                await CreateApplication("Log_Service", "'4taGNade4[4:,P[d*aHV]??PFRfH{9KaJ\\@gB}V%H");
                await CreateApplication("Admin_Panel", "naBqn07j18hxTZf*cdR~+t7ue1p]!9kf");
                await CreateApplication("File_Service", "'8t5[UNaT$a-$DC4JU*?(bxS=H<qXWk=`AZ:h@Bx@sn)r");
                await CreateApplication("Job_Service", "'5grW(Y-VMp%Y#.;Qc(}<[8~np!W\\,aknK@z#+dX_/)Gc");
                await CreateApplication("Notification_Service", "'X?DF9?)K{g[ux`*X27.9=YgyS!=\\87G7Cw~zL/{?X{Ee");
                await CreateApplication("MainApp", "'[c\\^[>4FBU4#~7~6C.B*.J-QaZ3jc.k;uv7aTX[,hP$");
                await CreateApplication("api_service", "Dp]04#,JDVR]A#Z1En:-cNtNjPPN#)*2)");

                await AddAdministratorRole("Admin_Panel");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task CreateApplication(string name, string secret)
		{
            if (await _userManager.FindByEmailAsync($"{name}@patika.com") != null)
                return;
            var userID = await IdentityApplicationService.RegisterApplicationAsync(new ApplicationRegistrationInputDto
            {
                Password = secret,
                ApplicationName = name
            });
            ApplicationUser user = await _userManager.FindByIdAsync(userID);
            await IdentityApplicationService.AddRoleToUserAsync(user, Consts.APPLICATION_CLIENT_ROLE);
        }

        private async Task CreateDefaultRolesAsync(IServiceProvider provider)
        {
            await CreateRoleAsync(provider, Consts.ADMINISTRATOR_ROLE);
            await CreateRoleAsync(provider, Consts.ANONYMOUS_ROLE);
            await CreateRoleAsync(provider, Consts.APPLICATION_CLIENT_ROLE);
            await CreateRoleAsync(provider, IPass.Shared.Consts.Consts.USER_ROLE); 
            await CreateRoleAsync(provider, IPass.Shared.Consts.Consts.USER_VALIDATED_ROLE);
        }

        private static async Task CreateRoleAsync(IServiceProvider provider, string role)
        {
            var manager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleExists = await manager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var newRole = new IdentityRole(role);
                await manager.CreateAsync(newRole);
            }
        }

        private async Task AddAdministratorRole(string name)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(name);

            if (!await _userManager.IsInRoleAsync(user, Consts.ADMINISTRATOR_ROLE))
                await IdentityApplicationService.AddRoleToUserAsync(user, Consts.ADMINISTRATOR_ROLE);
        }
    }
}

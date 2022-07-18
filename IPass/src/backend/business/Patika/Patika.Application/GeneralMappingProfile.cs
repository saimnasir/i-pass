using Patika.Application.Contracts;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities.Identity;

namespace Patika.Application
{
    public class GeneralMappingProfile : MappingProfile
    {
        protected override void Configure()
        { 
            CreateMapTwoSide<ApplicationUserDto, ApplicationUser>();             
        } 
    }
}

using IPass.Application.Mapper;
using IPass.Domain.CommonDomain.Entities;
using IPass.Domain.PasswordDomain.Entities;
using IPass.Shared.DTO.CommonDomain;
using IPass.Shared.DTO.PasswordDomain;
using Patika.Application.Contracts;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities.Identity;

namespace IPass.Application
{
    public class GeneralMappingProfile : MappingProfile
    {
        protected override void Configure()
        {
            CreateMapTwoSide<Organization, OrganizationDto>();
            CreateMapTwoSide<Organization, CreateOrganizationInputDto>();

            CreateMapTwoSide<OrganizationType, OrganizationTypeDto>();
            CreateMapTwoSide<OrganizationType, CreateOrganizationTypeInputDto>();

            CreateMapTwoSide<EnvironmentType, EnvironmentTypeDto>();
            CreateMapTwoSide<EnvironmentType, CreateEnvironmentTypeInputDto>();

            CreateMapTwoSide<MemoryType, MemoryTypeDto>();
            CreateMapTwoSide<MemoryType, CreateMemoryTypeInputDto>();

            CreateMapTwoSide<Memory, MemoryDto>();
            CreateMapTwoSide<Memory, CreateMemoryInputDto>();

            CreateMapTwoSide<ApplicationUserDto, ApplicationUser>();
            CreateMapTwoSide<UserDto, User>();

            MapPinCode();
        }

        private void MapPinCode()
        {
            CreateMapTwoSide<PinCode, PinCodeDto>();
            CreateMap<PinCode, PinCodeDto>()
              //.ForMember(d => d.Expired, opt => opt.ConvertUsing(new ReverseBoolConverter()))
              //.ForMember(d => d.Expired, opt => opt.ConvertUsing(new ExpirationConverter()))
               .ForMember(d => d.Expired, opt => opt.MapFrom(src => src.IsPinCodeExpired()))
              ;
        }
    }
}

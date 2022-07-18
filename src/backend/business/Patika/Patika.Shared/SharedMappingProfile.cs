using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Patika.Shared.DTO.Identity;
using Patika.Shared.Entities.Identity;

namespace Patika.Shared
{
    public class SharedMappingProfile 
    {
        MapperConfiguration Configuration { get; set; }
        MapperConfigurationExpression MapperConfiguration { get; set; }
        public IMapper Mapper { get; private set; }
        public SharedMappingProfile()
        {
            MapperConfiguration = new MapperConfigurationExpression();
            Configure();
            MapperConfiguration.AddCollectionMappers();
            Configuration = new MapperConfiguration(MapperConfiguration);
            Mapper = Configuration.CreateMapper();
        }

        protected  void Configure()
        {
            MapperConfiguration.CreateMap<ApplicationUser, ApplicationUserDto>();
            MapperConfiguration.CreateMap<ApplicationUserDto, ApplicationUser>();
        }
    }
}
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace Patika.Application.Contracts
{
    public abstract class MappingProfile
    {
        MapperConfiguration Configuration { get; set; }
        protected MapperConfigurationExpression MapperConfiguration { get; set; }
        public IMapper Mapper { get; private set; }

        public MappingProfile()
        {
            MapperConfiguration = new MapperConfigurationExpression();
            Configure();
            MapperConfiguration.AddCollectionMappers();
            Configuration = new MapperConfiguration(MapperConfiguration);
            Mapper = Configuration.CreateMapper();
        }

        protected abstract void Configure();

        protected IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>() => MapperConfiguration.CreateMap<TSource, TDestination>();
        protected void CreateMapTwoSide<X, Y>()
        {
            CreateMap<X, Y>();
            CreateMap<Y, X>();
        }

    }
}
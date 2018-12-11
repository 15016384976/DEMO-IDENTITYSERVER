using AutoMapper;
using IdentityServer4.Models;

namespace IdentityServer4.Dapper.Mappers
{
    public static class PersistedGrantMapper
    {
        internal static IMapper Mapper { get; }

        static PersistedGrantMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PersistedGrantMapperProfile>())
                .CreateMapper();
        }

        public static PersistedGrant ToModel(this Entities.PersistedGrant entity)
        {
            return entity == null ? null : Mapper.Map<PersistedGrant>(entity);
        }

        public static Entities.PersistedGrant ToEntity(this PersistedGrant model)
        {
            return model == null ? null : Mapper.Map<Entities.PersistedGrant>(model);
        }

        public static void UpdateEntity(this PersistedGrant model, Entities.PersistedGrant entity)
        {
            Mapper.Map(model, entity);
        }
    }

    public class PersistedGrantMapperProfile : Profile
    {
        public PersistedGrantMapperProfile()
        {
            CreateMap<Entities.PersistedGrant, PersistedGrant>(MemberList.Destination)
                .ReverseMap();
        }
    }
}

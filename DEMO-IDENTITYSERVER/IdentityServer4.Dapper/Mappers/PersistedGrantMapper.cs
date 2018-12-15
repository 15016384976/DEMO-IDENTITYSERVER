using AutoMapper;

namespace IdentityServer4.Dapper.Mappers
{
    public static class PersistedGrantMapper
    {
        internal static IMapper Mapper { get; }

        static PersistedGrantMapper()
        {
            Mapper = new MapperConfiguration(config => config.AddProfile<PersistedGrantMapperProfile>()).CreateMapper();
        }

        public static Models.PersistedGrant ToModel(this Entities.PersistedGrant entity)
        {
            return entity == null ? null : Mapper.Map<Models.PersistedGrant>(entity);
        }

        public static Entities.PersistedGrant ToEntity(this Models.PersistedGrant model)
        {
            return model == null ? null : Mapper.Map<Entities.PersistedGrant>(model);
        }
    }

    public class PersistedGrantMapperProfile : Profile
    {
        public PersistedGrantMapperProfile()
        {
            CreateMap<Entities.PersistedGrant, Models.PersistedGrant>(MemberList.Destination)
                .ConstructUsing(src => new Models.PersistedGrant())
                .ForMember(des => des.Key, exp => exp.MapFrom(src => src.K))
                .ForMember(des => des.Type, exp => exp.MapFrom(src => src.Type))
                .ForMember(des => des.SubjectId, exp => exp.MapFrom(src => src.SubjectId))
                .ForMember(des => des.ClientId, exp => exp.MapFrom(src => src.ClientId))
                .ForMember(des => des.CreationTime, exp => exp.MapFrom(src => src.CreationTime))
                .ForMember(des => des.Expiration, exp => exp.MapFrom(src => src.Expiration))
                .ForMember(des => des.Data, exp => exp.MapFrom(src => src.Data))
                .ReverseMap();
        }
    }
}

/*
static void PersistedGrantMapperTest()
{
    // IdentityServer4.Dapper.Entities.PersistedGrant -> IdentityServer4.Models.PersistedGrant
    var entityPersistedGrant = new IdentityServer4.Dapper.Entities.PersistedGrant();
    entityPersistedGrant.K = "K";
    entityPersistedGrant.Type = "Type";
    entityPersistedGrant.SubjectId = "SubjectId";
    entityPersistedGrant.ClientId = "ClientId";
    entityPersistedGrant.CreationTime = DateTime.Now;
    entityPersistedGrant.Expiration = DateTime.Now;
    entityPersistedGrant.Data = "Data";
    var model = entityPersistedGrant.ToModel();

    // IdentityServer4.Models.PersistedGrant -> IdentityServer4.Dapper.Entities.PersistedGrant
    var modelPersistedGrant = new IdentityServer4.Models.PersistedGrant();
    modelPersistedGrant.Key = "K";
    modelPersistedGrant.Type = "Type";
    modelPersistedGrant.SubjectId = "SubjectId";
    modelPersistedGrant.ClientId = "ClientId";
    modelPersistedGrant.CreationTime = DateTime.Now;
    modelPersistedGrant.Expiration = DateTime.Now;
    modelPersistedGrant.Data = "Data";
    var entity = modelPersistedGrant.ToEntity();
}
*/

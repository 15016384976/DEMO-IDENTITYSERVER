using AutoMapper;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Mappers
{
    public static class IdentityResrouceMapper
    {
        internal static IMapper Mapper { get; }

        static IdentityResrouceMapper()
        {
            Mapper = new MapperConfiguration(config => config.AddProfile<IdentityResourceMapperProfile>()).CreateMapper();
        }

        public static Models.IdentityResource ToModel(this Entities.IdentityResource entity)
        {
            return entity == null ? null : Mapper.Map<Models.IdentityResource>(entity);
        }

        public static Entities.IdentityResource ToEntity(this Models.IdentityResource model)
        {
            return model == null ? null : Mapper.Map<Entities.IdentityResource>(model);
        }
    }

    public class IdentityResourceMapperProfile : Profile
    {
        public IdentityResourceMapperProfile()
        {
            CreateMap<Entities.IdentityResource, Models.IdentityResource>(MemberList.Destination) // top-level
                .ConstructUsing(src => new Models.IdentityResource())
                .ForMember(des => des.Enabled, exp => exp.MapFrom(src => src.Enabled))
                .ForMember(des => des.Name, exp => exp.MapFrom(src => src.Name))
                .ForMember(des => des.DisplayName, exp => exp.MapFrom(src => src.DisplayName))
                .ForMember(des => des.Description, exp => exp.MapFrom(src => src.Description))
                .ForMember(des => des.Required, exp => exp.MapFrom(src => src.Required))
                .ForMember(des => des.Emphasize, exp => exp.MapFrom(src => src.Emphasize))
                .ForMember(des => des.ShowInDiscoveryDocument, exp => exp.MapFrom(src => src.ShowInDiscoveryDocument))
                .ForMember(des => des.UserClaims, exp => exp.MapFrom(src => src.IdentityClaims))
                .ForMember(des => des.Properties, exp => exp.MapFrom(src => src.IdentityResourceProperties))
                .ReverseMap();

            CreateMap<Entities.IdentityResourceClaim, string>()
                .ConstructUsing(src => src.Type)
                .ReverseMap()
                .ForMember(src => src.Type, exp => exp.MapFrom(des => des));

            CreateMap<Entities.IdentityResourceProperty, KeyValuePair<string, string>>()
                .ConstructUsing(src => new KeyValuePair<string, string>(src.K, src.V))
                .ReverseMap()
                .ForMember(src => src.K, exp => exp.MapFrom(des => des.Key))
                .ForMember(src => src.V, exp => exp.MapFrom(des => des.Value));
        }
    }
}

/*
static void IdentityResourceMapperTest()
{
    // IdentityServer4.Dapper.Entities.IdentityResource -> IdentityServer4.Models.IdentityResource
    var entityIdentityResource = new IdentityServer4.Dapper.Entities.IdentityResource();
    entityIdentityResource.Enabled = true;
    entityIdentityResource.Name = "Name";
    entityIdentityResource.DisplayName = "DisplayName";
    entityIdentityResource.Description = "Description";
    entityIdentityResource.Required = false;
    entityIdentityResource.Emphasize = false;
    entityIdentityResource.ShowInDiscoveryDocument = true;
    entityIdentityResource.IdentityClaims = new List<IdentityServer4.Dapper.Entities.IdentityResourceClaim>
    {
        new IdentityServer4.Dapper.Entities.IdentityResourceClaim { Id = 1, IdentityResourceId = 1, Type = "Type1" },
        new IdentityServer4.Dapper.Entities.IdentityResourceClaim { Id = 2, IdentityResourceId = 1, Type = "Type2" },
    };
    entityIdentityResource.IdentityResourceProperties = new List<IdentityServer4.Dapper.Entities.IdentityResourceProperty>
    {
        new IdentityServer4.Dapper.Entities.IdentityResourceProperty { Id = 1, IdentityResourceId = 1, K = "K1", V = "V1" },
        new IdentityServer4.Dapper.Entities.IdentityResourceProperty { Id = 2, IdentityResourceId = 1, K = "K2", V = "V2" },
    };
    var model = entityIdentityResource.ToModel();

    // IdentityServer4.Models.IdentityResource -> IdentityServer4.Dapper.Entities.IdentityResource
    var modelIdentityResource = new IdentityServer4.Models.IdentityResource();
    modelIdentityResource.Enabled = true;
    modelIdentityResource.Name = "Name";
    modelIdentityResource.DisplayName = "DisplayName";
    modelIdentityResource.Description = "Description";
    modelIdentityResource.Required = false;
    modelIdentityResource.Emphasize = false;
    modelIdentityResource.ShowInDiscoveryDocument = true;
    modelIdentityResource.UserClaims = new List<string> {
        "Type1",
        "Type2"
    };
    modelIdentityResource.Properties = new Dictionary<string, string>
    {
        { "K1", "V1" },
        { "K2", "V2" }
    };
    var entity = modelIdentityResource.ToEntity();
}
*/

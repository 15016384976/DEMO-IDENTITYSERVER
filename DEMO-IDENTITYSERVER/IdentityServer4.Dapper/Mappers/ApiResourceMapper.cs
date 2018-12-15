using AutoMapper;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Mappers
{
    public static class ApiResourceMapper
    {
        internal static IMapper Mapper { get; }

        static ApiResourceMapper()
        {
            Mapper = new MapperConfiguration(config => config.AddProfile<ApiResourceMapperProfile>()).CreateMapper();
        }

        public static Models.ApiResource ToModel(this Entities.ApiResource entity)
        {
            return entity == null ? null : Mapper.Map<Models.ApiResource>(entity);
        }

        public static Entities.ApiResource ToEntity(this Models.ApiResource model)
        {
            return model == null ? null : Mapper.Map<Entities.ApiResource>(model);
        }
    }

    public class ApiResourceMapperProfile : Profile
    {
        public ApiResourceMapperProfile()
        {
            CreateMap<Entities.ApiResource, Models.ApiResource>(MemberList.Destination) // top-level
                .ConstructUsing(src => new Models.ApiResource())
                .ForMember(des => des.Enabled, exp => exp.MapFrom(src => src.Enabled))
                .ForMember(des => des.Name, exp => exp.MapFrom(src => src.Name))
                .ForMember(des => des.DisplayName, exp => exp.MapFrom(src => src.DisplayName))
                .ForMember(des => des.Description, exp => exp.MapFrom(src => src.Description))
                .ForMember(des => des.UserClaims, exp => exp.MapFrom(src => src.ApiResourceClaims))
                .ForMember(des => des.Properties, exp => exp.MapFrom(src => src.ApiResourceProperties))
                .ForMember(des => des.Scopes, exp => exp.MapFrom(src => src.ApiResourceScopes))
                .ForMember(des => des.ApiSecrets, exp => exp.MapFrom(src => src.ApiResourceSecrets))
                .ReverseMap();

            CreateMap<Entities.ApiResourceClaim, string>()
                .ConstructUsing(src => src.Type)
                .ReverseMap()
                .ForMember(src => src.Type, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ApiResourceProperty, KeyValuePair<string, string>>()
                .ConstructUsing(src => new KeyValuePair<string, string>(src.K, src.V))
                .ReverseMap()
                .ForMember(src => src.K, exp => exp.MapFrom(des => des.Key))
                .ForMember(src => src.V, exp => exp.MapFrom(des => des.Value));

            CreateMap<Entities.ApiResourceScope, Models.Scope>(MemberList.Destination) // top-level
                .ConstructUsing(src => new Models.Scope())
                .ForMember(des => des.UserClaims, exp => exp.MapFrom(src => src.ApiResourceScopeClaims)) //
                .ReverseMap()
                .ForMember(src => src.Name, exp => exp.MapFrom(des => des.Name))
                .ForMember(src => src.DisplayName, exp => exp.MapFrom(des => des.DisplayName))
                .ForMember(src => src.Description, exp => exp.MapFrom(des => des.Description))
                .ForMember(src => src.Required, exp => exp.MapFrom(des => des.Required))
                .ForMember(src => src.Emphasize, exp => exp.MapFrom(des => des.Emphasize))
                .ForMember(src => src.ShowInDiscoveryDocument, exp => exp.MapFrom(des => des.ShowInDiscoveryDocument))
                .ForMember(src => src.ApiResourceScopeClaims, exp => exp.MapFrom(des => des.UserClaims));

            CreateMap<Entities.ApiResourceScopeClaim, string>()
                .ConstructUsing(src => src.Type)
                .ReverseMap()
                .ForMember(src => src.Type, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ApiResourceSecret, Models.Secret>(MemberList.Destination)
                .ConstructUsing(src => new Models.Secret())
                .ReverseMap()
                .ForMember(src => src.Description, exp => exp.MapFrom(des => des.Description))
                .ForMember(src => src.Value, exp => exp.MapFrom(des => des.Value))
                .ForMember(src => src.Expiration, exp => exp.MapFrom(des => des.Expiration))
                .ForMember(src => src.Type, exp => exp.MapFrom(des => des.Type));
        }
    }

    
}

/*
static void ApiResourceMapperTest()
{
    // IdentityServer4.Dapper.Entities.ApiResource -> IdentityServer4.Models.ApiResource
    var entityApiResource = new IdentityServer4.Dapper.Entities.ApiResource();
    entityApiResource.Id = 1;
    entityApiResource.Enabled = true;
    entityApiResource.Name = "Name";
    entityApiResource.DisplayName = "DisplayName";
    entityApiResource.Description = "Description";
    entityApiResource.ApiResourceClaims = new List<IdentityServer4.Dapper.Entities.ApiResourceClaim>
    {
        new IdentityServer4.Dapper.Entities.ApiResourceClaim { Id = 1, ApiResourceId = 1, Type ="Claim1" },
        new IdentityServer4.Dapper.Entities.ApiResourceClaim { Id = 2, ApiResourceId = 1, Type ="Claim2" }
    };
    entityApiResource.ApiResourceProperties = new List<IdentityServer4.Dapper.Entities.ApiResourceProperty>
    {
        new IdentityServer4.Dapper.Entities.ApiResourceProperty { Id = 1, ApiResourceId = 1, K = "K1", V = "V1" },
        new IdentityServer4.Dapper.Entities.ApiResourceProperty { Id = 2, ApiResourceId = 1, K = "K2", V = "V2" }
    };
    entityApiResource.ApiResourceScopes = new List<IdentityServer4.Dapper.Entities.ApiResourceScope>
    {
        new IdentityServer4.Dapper.Entities.ApiResourceScope {
            Id = 1,
            ApiResourceId = 1,
            Name = "Name1",
            DisplayName = "DisplayName1",
            Description = "Description1",
            Required = false,
            Emphasize = false,
            ShowInDiscoveryDocument = true,
            ApiResourceScopeClaims = new List<IdentityServer4.Dapper.Entities.ApiResourceScopeClaim> {
                new IdentityServer4.Dapper.Entities.ApiResourceScopeClaim
                {
                    Id = 1,
                    ApiResourceScopeId = 1,
                    Type = "Type1"
                }
            }
        }
    };
    entityApiResource.ApiResourceSecrets = new List<IdentityServer4.Dapper.Entities.ApiResourceSecret>
    {
        new IdentityServer4.Dapper.Entities.ApiResourceSecret { Id = 1, ApiResourceId = 1, Description = "Description1", Value = "Value1", Expiration = DateTime.Now, Type = "SharedSecret" }
    };
    var model = entityApiResource.ToModel();

    // IdentityServer4.Models.ApiResource -> IdentityServer4.Dapper.Entities.ApiResource
    var modelApiResource = new IdentityServer4.Models.ApiResource();
    modelApiResource.Enabled = true;
    modelApiResource.Name = "Name";
    modelApiResource.DisplayName = "DisplayName";
    modelApiResource.Description = "Description";
    modelApiResource.UserClaims = new List<string> { "Claim1", "Claim2" };
    modelApiResource.Properties = new Dictionary<string, string> { { "K1", "V1" }, { "K2", "V2" } };
    modelApiResource.Scopes = new List<IdentityServer4.Models.Scope>
    {
        new IdentityServer4.Models.Scope {
            Name = "Name1",
            DisplayName = "DisplayName1",
            Description = "Description1",
            Required = false,
            Emphasize = false,
            ShowInDiscoveryDocument = true,
            UserClaims = new List<string> {
                "Type1"
            }
        }
    };
    modelApiResource.ApiSecrets = new List<IdentityServer4.Models.Secret>
    {
        new IdentityServer4.Models.Secret { Description = "Description1", Value = "Value1", Expiration = DateTime.Now, Type = "SharedSecret" }
    };
    var entity = modelApiResource.ToEntity();
}
*/

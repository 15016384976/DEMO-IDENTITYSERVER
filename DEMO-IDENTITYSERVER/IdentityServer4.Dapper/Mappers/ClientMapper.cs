using AutoMapper;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Mappers
{
    public static class ClientMapper
    {
        internal static IMapper Mapper { get; }

        static ClientMapper()
        {
            Mapper = new MapperConfiguration(config => config.AddProfile<ClientMapperProfile>()).CreateMapper();
        }

        public static Models.Client ToModel(this Entities.Client entity)
        {
            return entity == null ? null : Mapper.Map<Models.Client>(entity);
        }

        public static Entities.Client ToEntity(this Models.Client model)
        {
            return model == null ? null : Mapper.Map<Entities.Client>(model);
        }
    }

    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            CreateMap<Entities.Client, Models.Client>()
                .ConstructUsing(src => new Models.Client()) // top-level
                .ForMember(des => des.Enabled, exp => exp.MapFrom(src => src.Enabled))
                .ForMember(des => des.ClientId, exp => exp.MapFrom(src => src.ClientId))
                .ForMember(des => des.ProtocolType, exp => exp.MapFrom(src => src.ProtocolType))
                .ForMember(des => des.RequireClientSecret, exp => exp.MapFrom(src => src.RequireClientSecret))
                .ForMember(des => des.ClientName, exp => exp.MapFrom(src => src.ClientName))
                .ForMember(des => des.Description, exp => exp.MapFrom(src => src.Description))
                .ForMember(des => des.ClientUri, exp => exp.MapFrom(src => src.ClientUri))
                .ForMember(des => des.LogoUri, exp => exp.MapFrom(src => src.LogoUri))
                .ForMember(des => des.RequireConsent, exp => exp.MapFrom(src => src.RequireConsent))
                .ForMember(des => des.AllowRememberConsent, exp => exp.MapFrom(src => src.AllowRememberConsent))
                .ForMember(des => des.AlwaysIncludeUserClaimsInIdToken, exp => exp.MapFrom(src => src.AlwaysIncludeUserClaimsInIdToken))
                .ForMember(des => des.RequirePkce, exp => exp.MapFrom(src => src.RequirePkce))
                .ForMember(des => des.AllowPlainTextPkce, exp => exp.MapFrom(src => src.AllowPlainTextPkce))
                .ForMember(des => des.AllowAccessTokensViaBrowser, exp => exp.MapFrom(src => src.AllowAccessTokensViaBrowser))
                .ForMember(des => des.FrontChannelLogoutUri, exp => exp.MapFrom(src => src.FrontChannelLogoutUri))
                .ForMember(des => des.FrontChannelLogoutSessionRequired, exp => exp.MapFrom(src => src.FrontChannelLogoutSessionRequired))
                .ForMember(des => des.BackChannelLogoutUri, exp => exp.MapFrom(src => src.BackChannelLogoutUri))
                .ForMember(des => des.BackChannelLogoutSessionRequired, exp => exp.MapFrom(src => src.BackChannelLogoutSessionRequired))
                .ForMember(des => des.AllowOfflineAccess, exp => exp.MapFrom(src => src.AllowOfflineAccess))
                .ForMember(des => des.IdentityTokenLifetime, exp => exp.MapFrom(src => src.IdentityTokenLifetime))
                .ForMember(des => des.AccessTokenLifetime, exp => exp.MapFrom(src => src.AccessTokenLifetime))
                .ForMember(des => des.AuthorizationCodeLifetime, exp => exp.MapFrom(src => src.AuthorizationCodeLifetime))
                .ForMember(des => des.ConsentLifetime, exp => exp.MapFrom(src => src.ConsentLifetime))
                .ForMember(des => des.AbsoluteRefreshTokenLifetime, exp => exp.MapFrom(src => src.AbsoluteRefreshTokenLifetime))
                .ForMember(des => des.SlidingRefreshTokenLifetime, exp => exp.MapFrom(src => src.SlidingRefreshTokenLifetime))
                .ForMember(des => des.RefreshTokenUsage, exp => exp.MapFrom(src => src.RefreshTokenUsage))
                .ForMember(des => des.UpdateAccessTokenClaimsOnRefresh, exp => exp.MapFrom(src => src.UpdateAccessTokenClaimsOnRefresh))
                .ForMember(des => des.RefreshTokenExpiration, exp => exp.MapFrom(src => src.RefreshTokenExpiration))
                .ForMember(des => des.AccessTokenType, exp => exp.MapFrom(src => src.AccessTokenType))
                .ForMember(des => des.EnableLocalLogin, exp => exp.MapFrom(src => src.EnableLocalLogin))
                .ForMember(des => des.IncludeJwtId, exp => exp.MapFrom(src => src.IncludeJwtId))
                .ForMember(des => des.AlwaysSendClientClaims, exp => exp.MapFrom(src => src.AlwaysSendClientClaims))
                .ForMember(des => des.ClientClaimsPrefix, exp => exp.MapFrom(src => src.ClientClaimsPrefix))
                .ForMember(des => des.PairWiseSubjectSalt, exp => exp.MapFrom(src => src.PairWiseSubjectSalt))
                .ForMember(des => des.UserSsoLifetime, exp => exp.MapFrom(src => src.UserSsoLifetime))
                .ForMember(des => des.UserCodeType, exp => exp.MapFrom(src => src.UserCodeType))
                .ForMember(des => des.DeviceCodeLifetime, exp => exp.MapFrom(src => src.DeviceCodeLifetime))
                .ForMember(des => des.Claims, exp => exp.MapFrom(src => src.ClientClaims))
                .ForMember(des => des.AllowedCorsOrigins, exp => exp.MapFrom(src => src.ClientCorsOrigins))
                .ForMember(des => des.AllowedGrantTypes, exp => exp.MapFrom(src => src.ClientGrantTypes))
                .ForMember(des => des.IdentityProviderRestrictions, exp => exp.MapFrom(src => src.ClientIdPRestrictions))
                .ForMember(des => des.PostLogoutRedirectUris, exp => exp.MapFrom(src => src.ClientPostLogoutRedirectUris))
                .ForMember(des => des.Properties, exp => exp.MapFrom(src => src.ClientProperties))
                .ForMember(des => des.RedirectUris, exp => exp.MapFrom(src => src.ClientRedirectUris))
                .ForMember(des => des.AllowedScopes, exp => exp.MapFrom(src => src.ClientScopes))
                .ForMember(des => des.ClientSecrets, exp => exp.MapFrom(src => src.ClientSecrets))
                .ReverseMap();

            CreateMap<Entities.ClientClaim, System.Security.Claims.Claim>(MemberList.Destination)
                .ConstructUsing(src => new System.Security.Claims.Claim(src.Type, src.Value))
                .ReverseMap()
                .ForMember(src => src.Type, exp => exp.MapFrom(des => des.Type))
                .ForMember(src => src.Value, exp => exp.MapFrom(des => des.Value));

            CreateMap<Entities.ClientCorsOrigin, string>()
                .ConstructUsing(src => src.Origin)
                .ReverseMap()
                .ForMember(src => src.Origin, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ClientGrantType, string>()
                .ConstructUsing(src => src.GrantType)
                .ReverseMap()
                .ForMember(src => src.GrantType, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ClientIdPRestriction, string>()
                .ConstructUsing(src => src.Provider)
                .ReverseMap()
                .ForMember(src => src.Provider, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ClientPostLogoutRedirectUri, string>()
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(src => src.PostLogoutRedirectUri, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ClientProperty, KeyValuePair<string, string>>()
                .ConstructUsing(src => new KeyValuePair<string, string>(src.K, src.V))
                .ReverseMap()
                .ForMember(src => src.K, exp => exp.MapFrom(des => des.Key))
                .ForMember(src => src.V, exp => exp.MapFrom(des => des.Value));

            CreateMap<Entities.ClientRedirectUri, string>()
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(src => src.RedirectUri, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ClientScope, string>()
                .ConstructUsing(src => src.Scope)
                .ReverseMap()
                .ForMember(src => src.Scope, exp => exp.MapFrom(des => des));

            CreateMap<Entities.ClientSecret, Models.Secret>(MemberList.Destination)
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
static void ClientMapperTest()
{
    // IdentityServer4.Dapper.Entities.Client -> IdentityServer4.Models.Client
    var entityClient = new IdentityServer4.Dapper.Entities.Client();
    entityClient.Id = 1;
    entityClient.Enabled = true;
    entityClient.ClientId = "ClientId";
    entityClient.ProtocolType = "oidc";
    entityClient.RequireClientSecret = true;
    entityClient.ClientName = "ClientName";
    entityClient.Description = "Description";
    entityClient.ClientUri = "ClientUri";
    entityClient.LogoUri = "LogoUri";
    entityClient.RequireConsent = true;
    entityClient.AllowRememberConsent = true;
    entityClient.AlwaysIncludeUserClaimsInIdToken = false;
    entityClient.RequirePkce = false;
    entityClient.AllowPlainTextPkce = false;
    entityClient.AllowAccessTokensViaBrowser = false;
    entityClient.FrontChannelLogoutUri = "FrontChannelLogoutUri";
    entityClient.FrontChannelLogoutSessionRequired = true;
    entityClient.BackChannelLogoutUri = "BackChannelLogoutUri";
    entityClient.BackChannelLogoutSessionRequired = true;
    entityClient.AllowOfflineAccess = false;
    entityClient.IdentityTokenLifetime = 300;
    entityClient.AccessTokenLifetime = 3600;
    entityClient.AuthorizationCodeLifetime = 300;
    entityClient.ConsentLifetime = 0;
    entityClient.AbsoluteRefreshTokenLifetime = 2592000;
    entityClient.SlidingRefreshTokenLifetime = 1296000;
    entityClient.RefreshTokenUsage = 1;
    entityClient.UpdateAccessTokenClaimsOnRefresh = false;
    entityClient.RefreshTokenExpiration = 1;
    entityClient.AccessTokenType = 0;
    entityClient.EnableLocalLogin = true;
    entityClient.IncludeJwtId = false;
    entityClient.AlwaysSendClientClaims = false;
    entityClient.ClientClaimsPrefix = "client_";
    entityClient.PairWiseSubjectSalt = "PairWiseSubjectSalt";
    entityClient.UserSsoLifetime = 0;
    entityClient.UserCodeType = "UserCodeType";
    entityClient.DeviceCodeLifetime = 300;
    entityClient.ClientClaims = new List<IdentityServer4.Dapper.Entities.ClientClaim>
    {
        new IdentityServer4.Dapper.Entities.ClientClaim { Id = 1, ClientId = 1, Type = "Type1", Value = "Value1" }
    };
    entityClient.ClientCorsOrigins = new List<IdentityServer4.Dapper.Entities.ClientCorsOrigin>
    {
        new IdentityServer4.Dapper.Entities.ClientCorsOrigin { Id = 1, ClientId = 1, Origin = "Origin1" }
    };
    entityClient.ClientGrantTypes = new List<IdentityServer4.Dapper.Entities.ClientGrantType>
    {
        new IdentityServer4.Dapper.Entities.ClientGrantType { Id = 1, ClientId = 1, GrantType = "GrantType1" }
    };
    entityClient.ClientIdPRestrictions = new List<IdentityServer4.Dapper.Entities.ClientIdPRestriction>
    {
        new IdentityServer4.Dapper.Entities.ClientIdPRestriction { Id = 1, ClientId = 1, Provider = "Provider1" }
    };
    entityClient.ClientPostLogoutRedirectUris = new List<IdentityServer4.Dapper.Entities.ClientPostLogoutRedirectUri>
    {
        new IdentityServer4.Dapper.Entities.ClientPostLogoutRedirectUri { Id = 1, ClientId = 1, PostLogoutRedirectUri = "PostLogoutRedirectUri1" }
    };
    entityClient.ClientProperties = new List<IdentityServer4.Dapper.Entities.ClientProperty>
    {
        new IdentityServer4.Dapper.Entities.ClientProperty { Id = 1, ClientId = 1, K = "K1", V = "V1" }
    };
    entityClient.ClientRedirectUris = new List<IdentityServer4.Dapper.Entities.ClientRedirectUri>
    {
        new IdentityServer4.Dapper.Entities.ClientRedirectUri { Id = 1, ClientId = 1, RedirectUri = "RedirectUri1" }
    };
    entityClient.ClientScopes = new List<IdentityServer4.Dapper.Entities.ClientScope>
    {
        new IdentityServer4.Dapper.Entities.ClientScope { Id = 1, ClientId = 1, Scope = "Scope1" }
    };
    entityClient.ClientSecrets = new List<IdentityServer4.Dapper.Entities.ClientSecret>
    {
        new IdentityServer4.Dapper.Entities.ClientSecret { Id = 1, ClientId = 1, Description = "Description1", Value = "Value1", Expiration = DateTime.Now, Type = "SharedSecret" }
    };
    var model = entityClient.ToModel();

    // IdentityServer4.Models.Client -> IdentityServer4.Dapper.Entities.Client
    var modelClient = new IdentityServer4.Models.Client();
    modelClient.Enabled = true;
    modelClient.ClientId = "ClientId";
    modelClient.ProtocolType = "oidc";
    modelClient.RequireClientSecret = true;
    modelClient.ClientName = "ClientName";
    modelClient.Description = "Description";
    modelClient.ClientUri = "ClientUri";
    modelClient.LogoUri = "LogoUri";
    modelClient.RequireConsent = true;
    modelClient.AllowRememberConsent = true;
    modelClient.AlwaysIncludeUserClaimsInIdToken = false;
    modelClient.RequirePkce = false;
    modelClient.AllowPlainTextPkce = false;
    modelClient.AllowAccessTokensViaBrowser = false;
    modelClient.FrontChannelLogoutUri = "FrontChannelLogoutUri";
    modelClient.FrontChannelLogoutSessionRequired = true;
    modelClient.BackChannelLogoutUri = "BackChannelLogoutUri";
    modelClient.BackChannelLogoutSessionRequired = true;
    modelClient.AllowOfflineAccess = false;
    modelClient.IdentityTokenLifetime = 300;
    modelClient.AccessTokenLifetime = 3600;
    modelClient.AuthorizationCodeLifetime = 300;
    modelClient.ConsentLifetime = 0;
    modelClient.AbsoluteRefreshTokenLifetime = 2592000;
    modelClient.SlidingRefreshTokenLifetime = 1296000;
    modelClient.RefreshTokenUsage = IdentityServer4.Models.TokenUsage.OneTimeOnly;
    modelClient.UpdateAccessTokenClaimsOnRefresh = false;
    modelClient.RefreshTokenExpiration = IdentityServer4.Models.TokenExpiration.Absolute;
    modelClient.AccessTokenType = 0;
    modelClient.EnableLocalLogin = true;
    modelClient.IncludeJwtId = false;
    modelClient.AlwaysSendClientClaims = false;
    modelClient.ClientClaimsPrefix = "client_";
    modelClient.PairWiseSubjectSalt = "PairWiseSubjectSalt";
    modelClient.UserSsoLifetime = 0;
    modelClient.UserCodeType = "UserCodeType";
    modelClient.DeviceCodeLifetime = 300;
    modelClient.Claims = new List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim("Type1", "Value1")
    };
    modelClient.AllowedCorsOrigins = new List<string>
    {
        "Origin1"
    };
    modelClient.AllowedGrantTypes = new List<string>
    {
        "GrantType1"
    };
    modelClient.IdentityProviderRestrictions = new List<string>
    {
        "Provider1"
    };
    modelClient.PostLogoutRedirectUris = new List<string>
    {
        "PostLogoutRedirectUri1"
    };
    modelClient.Properties = new Dictionary<string, string>
    {
        { "K1", "V1" }
    };
    modelClient.RedirectUris = new List<string>
    {
        "RedirectUri1"
    };
    modelClient.AllowedScopes = new List<string>
    {
        "Scope1"
    };
    modelClient.ClientSecrets = new List<IdentityServer4.Models.Secret>
    {
        new IdentityServer4.Models.Secret { Description = "Description1", Value = "Value1", Expiration = DateTime.Now, Type = "SharedSecret" }
    };
    var entity = modelClient.ToEntity();
}
*/

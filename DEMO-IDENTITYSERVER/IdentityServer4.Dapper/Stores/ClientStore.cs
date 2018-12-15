using Dapper;
using IdentityServer4.Dapper.Mappers;
using IdentityServer4.Stores;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly DapperStoreOptions _dapperStoreOptions;

        public ClientStore(DapperStoreOptions dapperStoreOptions)
        {
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<Models.Client> FindClientByIdAsync(string clientId)
        {
            var entity = new Entities.Client();
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT
	                Id,
	                Enabled,
	                ClientId,
	                ProtocolType,
	                RequireClientSecret,
	                ClientName,
	                Description,
	                ClientUri,
	                LogoUri,
	                RequireConsent,
	                AllowRememberConsent,
	                AlwaysIncludeUserClaimsInIdToken,
	                RequirePkce,
	                AllowPlainTextPkce,
	                AllowAccessTokensViaBrowser,
	                FrontChannelLogoutUri,
	                FrontChannelLogoutSessionRequired,
	                BackChannelLogoutUri,
	                BackChannelLogoutSessionRequired,
	                AllowOfflineAccess,
	                IdentityTokenLifetime,
	                AccessTokenLifetime,
	                AuthorizationCodeLifetime,
	                ConsentLifetime,
	                AbsoluteRefreshTokenLifetime,
	                SlidingRefreshTokenLifetime,
	                RefreshTokenUsage,
	                UpdateAccessTokenClaimsOnRefresh,
	                RefreshTokenExpiration,
	                AccessTokenType,
	                EnableLocalLogin,
	                IncludeJwtId,
	                AlwaysSendClientClaims,
	                ClientClaimsPrefix,
	                PairWiseSubjectSalt,
	                UserSsoLifetime,
	                UserCodeType,
	                DeviceCodeLifetime
                FROM Client
                WHERE ClientId = @clientId;

                SELECT
	                A.Id,
                    A.ClientId,
                    A.Type,
                    A.Value
                FROM ClientClaim AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
	                A.Id,
                    A.ClientId,
                    A.Origin
                FROM ClientCorsOrigin AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
	                A.Id,
                    A.ClientId,
                    A.GrantType
                FROM ClientGrantType AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
                    A.Id,
                    A.ClientId,
                    A.Provider
                FROM ClientIdPRestriction AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
                    A.Id,
                    A.ClientId,
                    A.PostLogoutRedirectUri 
				FROM ClientPostLogoutRedirectUri AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
                    A.Id,
                    A.ClientId,
                    A.K,
                    A.V
                FROM ClientProperty AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
                    A.Id,
                    A.ClientId,
                    A.RedirectUri
                FROM ClientRedirectUri AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
                    A.Id,
                    A.ClientId,
                    A.Scope
                FROM ClientScope AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;

                SELECT
                    A.Id,
                    A.ClientId,
                    A.Description,
                    A.Value,
                    A.Expiration,
                    A.Type
                FROM ClientSecret AS A
                INNER JOIN Client AS B ON A.ClientId = B.Id
                WHERE B.ClientId = @clientId;
                ";
                var reader = await connection.QueryMultipleAsync(sql, new { clientId });

                var entityClients = reader.Read<Entities.Client>();
                var entityClientClaims = reader.Read<Entities.ClientClaim>();
                var entityClientCorsOrigins = reader.Read<Entities.ClientCorsOrigin>();
                var entityClientGrantTypes = reader.Read<Entities.ClientGrantType>();
                var entityClientIdPRestrictions = reader.Read<Entities.ClientIdPRestriction>();
                var entityClientPostLogoutRedirectUris = reader.Read<Entities.ClientPostLogoutRedirectUri>();
                var entityClientProperties = reader.Read<Entities.ClientProperty>();
                var entityClientRedirectUris = reader.Read<Entities.ClientRedirectUri>();
                var entityClientScopes = reader.Read<Entities.ClientScope>();
                var entityClientSecrets = reader.Read<Entities.ClientSecret>();

                if (entityClients != null && entityClients.AsList().Count > 0)
                {
                    entity = entityClients.AsList()[0];
                    entity.ClientClaims = entityClientClaims.AsList();
                    entity.ClientCorsOrigins = entityClientCorsOrigins.AsList();
                    entity.ClientGrantTypes = entityClientGrantTypes.AsList();
                    entity.ClientIdPRestrictions = entityClientIdPRestrictions.AsList();
                    entity.ClientPostLogoutRedirectUris = entityClientPostLogoutRedirectUris.AsList();
                    entity.ClientProperties = entityClientProperties.AsList();
                    entity.ClientRedirectUris = entityClientRedirectUris.AsList();
                    entity.ClientScopes = entityClientScopes.AsList();
                    entity.ClientSecrets = entityClientSecrets.AsList();

                    return entity.ToModel();
                }
            }
            return null;
        }
    }
}

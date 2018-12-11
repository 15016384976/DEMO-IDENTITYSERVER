using Dapper;
using IdentityServer4.Dapper.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly ILogger<ClientStore> _logger;
        private readonly DapperStoreOptions _dapperStoreOptions;

        public ClientStore(ILogger<ClientStore> logger, DapperStoreOptions dapperStoreOptions)
        {
            _logger = logger;
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var result = new Client();
            var entity = new Entities.Client();
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                //由于后续未用到，暂不实现 ClientPostLogoutRedirectUris ClientClaims ClientIdPRestrictions ClientCorsOrigins ClientProperties,有需要的自行添加。
                var sqlStr = @"
                SELECT
	                *
                FROM Client
                WHERE ClientId = @clientId AND Enabled = 1;

                SELECT 
	                B.* 
                FROM Client AS A 
                INNER JOIN ClientGrantType AS B ON A.Id = B.ClientId 
                WHERE A.ClientId = @clientId and A.Enabled = 1;

                SELECT 
	                B.* 
                FROM Client AS A 
                INNER JOIN ClientRedirectUri AS B ON A.Id = B.ClientId 
                WHERE A.ClientId = @clientId and A.Enabled = 1;

                SELECT 
	                B.* 
                FROM Client AS A 
                INNER JOIN ClientScope AS B ON A.Id = B.ClientId 
                WHERE A.ClientId = @clientId and A.Enabled = 1;

                SELECT 
	                B.* 
                FROM Client AS A 
                INNER JOIN ClientSecret AS B ON A.Id = B.ClientId 
                WHERE A.ClientId = @clientId and A.Enabled = 1;
                ";

                var reader = await connection.QueryMultipleAsync(sqlStr, new { clientId });

                var entityClient = reader.Read<Entities.Client>();
                var entityClientGrantType = reader.Read<Entities.ClientGrantType>();
                var entityClientRedirectUri = reader.Read<Entities.ClientRedirectUri>();
                var entityClientScope = reader.Read<Entities.ClientScope>();
                var entityClientSecret = reader.Read<Entities.ClientSecret>();

                if (entityClient != null && entityClient.AsList().Count > 0)
                {
                    entity = entityClient.AsList()[0];
                    entity.AllowedGrantTypes = entityClientGrantType.AsList();
                    entity.RedirectUris = entityClientRedirectUri.AsList();
                    entity.AllowedScopes = entityClientScope.AsList();
                    entity.ClientSecrets = entityClientSecret.AsList();

                    result = entity.ToModel();
                }
            }

            _logger.LogDebug("{clientId} found in database: {clientIdFound}", clientId, entity != null);

            return result;
        }
    }
}

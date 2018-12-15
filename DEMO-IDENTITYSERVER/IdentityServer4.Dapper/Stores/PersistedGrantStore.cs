using Dapper;
using IdentityServer4.Dapper.Mappers;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly DapperStoreOptions _dapperStoreOptions;

        public PersistedGrantStore(DapperStoreOptions dapperStoreOptions)
        {
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<IEnumerable<Models.PersistedGrant>> GetAllAsync(string subjectId)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                string sql = $@"
                SELECT 
	                K,
	                Type,
	                SubjectId,
	                ClientId,
	                CreationTime,
	                Expiration,
	                Data 
                FROM PersistedGrant 
                WHERE SubjectId = @subjectId;
                ";
                var persistedGrants = (await connection.QueryAsync<Entities.PersistedGrant>(sql, new { subjectId }))?.AsList();
                return persistedGrants.Select(v => v.ToModel());
            }
        }

        public async Task<Models.PersistedGrant> GetAsync(string key)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT 
	                K,
	                Type,
	                SubjectId,
	                ClientId,
	                CreationTime,
	                Expiration,
	                Data 
                FROM PersistedGrant 
                WHERE K = @key;
                ";
                var persistedGrant = await connection.QueryFirstOrDefaultAsync<Entities.PersistedGrant>(sql, new { key });
                return persistedGrant?.ToModel();
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                DELETE 
                FROM PersistedGrant 
                WHERE ClientId = @clientId AND SubjectId = @subjectId;
                ";
                await connection.ExecuteAsync(sql, new { subjectId, clientId });
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                DELETE 
                FROM PersistedGrant 
                WHERE ClientId = @clientId AND SubjectId = @subjectId AND Type = @type;
                ";
                await connection.ExecuteAsync(sql, new { subjectId, clientId, type });
            }
        }

        public async Task RemoveAsync(string key)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                DELETE 
                FROM PersistedGrant
                WHERE K = @key;
                ";
                await connection.ExecuteAsync(sql, new { key });
            }
        }

        public async Task StoreAsync(Models.PersistedGrant grant)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                await RemoveAsync(grant.Key);
                var sql = $@"
                INSERT 
                INTO PersistedGrant(K, Type, SubjectId, ClientId, CreationTime, Expiration, Data) 
                VALUES(@Key, @Type, @SubjectId, @ClientId, @CreationTime, @Expiration, @Data);
                ";
                await connection.ExecuteAsync(sql, new { grant.Key, grant.Type, grant.SubjectId, grant.ClientId, grant.CreationTime, grant.Expiration, grant.Data });
            }
        }
    }
}

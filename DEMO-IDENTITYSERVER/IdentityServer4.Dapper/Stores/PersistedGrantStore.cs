using Dapper;
using IdentityServer4.Dapper.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly ILogger<PersistedGrantStore> _logger;
        private readonly DapperStoreOptions _dapperStoreOptions;

        public PersistedGrantStore(ILogger<PersistedGrantStore> logger, DapperStoreOptions dapperStoreOptions)
        {
            _logger = logger;
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                string sqlStr = $@"
                SELECT 
	                * 
                FROM PersistedGrant 
                WHERE SubjectId = @subjectId
                ";

                var persistedGrants = (await connection.QueryAsync<Entities.PersistedGrant>(sqlStr, new { subjectId }))?.AsList();

                _logger.LogDebug("{persistedGrantCount} persisted grants found for {subjectId}", persistedGrants.Count, subjectId);

                return persistedGrants.Select(x => x.ToModel());
            }
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                SELECT
	                * 
                FROM PersistedGrant 
                WHERE [Key] = @key
                ";

                var persistedGrant = await connection.QueryFirstOrDefaultAsync<Entities.PersistedGrant>(sqlStr, new { key });

                var result = persistedGrant.ToModel();

                _logger.LogDebug("{persistedGrantKey} found in database: {persistedGrantKeyFound}", key, result != null);

                return result;
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                DELETE 
                FROM PersistedGrant 
                WHERE ClientId = @clientId AND SubjectId = @subjectId
                ";

                await connection.ExecuteAsync(sqlStr, new { subjectId, clientId });

                _logger.LogDebug("remove {subjectId} {clientId} from database success", subjectId, clientId);
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                DELETE 
                FROM PersistedGrant 
                WHERE ClientId = @clientId AND SubjectId = @subjectId AND Type = @type
                ";

                await connection.ExecuteAsync(sqlStr, new { subjectId, clientId, type });

                _logger.LogDebug("remove {subjectId} {clientId} {type} from database success", subjectId, clientId, type);
            }
        }

        public async Task RemoveAsync(string key)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                DELETE 
                FROM PersistedGrant
                WHERE[Key] = @key
                ";

                await connection.ExecuteAsync(sqlStr, new { key });

                _logger.LogDebug("remove {key} from database success", key);
            }
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                await RemoveAsync(grant.Key);

                var sqlStr = $@"
                INSERT 
                INTO PersistedGrant([Key], Type, SubjectId, ClientId, CreationTime, Expiration, Data) 
                VALUES(@Key, @Type, @SubjectId, @ClientId, @CreationTime, @Expiration, @Data)
                ";

                await connection.ExecuteAsync(sqlStr, grant);
            }
        }
    }

    public interface IPersistedGrantExpiredCleanup
    {
        Task Cleanup(DateTime dateTime);
    }

    public class PersistedGrantExpiredCleanup : IPersistedGrantExpiredCleanup
    {
        private readonly ILogger<PersistedGrantExpiredCleanup> _logger;
        private readonly DapperStoreOptions _dapperStoreOptions;

        public PersistedGrantExpiredCleanup(ILogger<PersistedGrantExpiredCleanup> logger, DapperStoreOptions dapperStoreOptions)
        {
            _logger = logger;
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task Cleanup(DateTime dateTime)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                DELETE 
                FROM PersistedGrant 
                WHERE Expiration > @dateTime";

                await connection.ExecuteAsync(sqlStr, new { dateTime });
            }
        }
    }
}

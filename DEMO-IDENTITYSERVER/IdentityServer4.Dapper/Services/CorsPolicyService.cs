using Dapper;
using IdentityServer4.Services;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly DapperStoreOptions _dapperStoreOptions;

        public CorsPolicyService(DapperStoreOptions dapperStoreOptions)
        {
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT DISTINCT 
                    Origin
                FROM ClientCorsOrigin AS A
                LEFT JOIN Client AS B ON A.ClientId = B.Id;
                ";
                var origins = (await connection.QueryAsync<string>(sql))?.AsList();
                return origins.Contains(origin, StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}

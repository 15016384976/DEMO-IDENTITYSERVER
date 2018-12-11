using Dapper;
using IdentityServer4.Dapper.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly ILogger<ResourceStore> _logger;
        private readonly DapperStoreOptions _dapperStoreOptions;

        public ResourceStore(ILogger<ResourceStore> logger, DapperStoreOptions dapperStoreOptions)
        {
            _logger = logger;
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var result = new ApiResource();

            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                SELECT
	                * 
                FROM ApiResource 
                WHERE Name = @name AND Enabled = 1;

                SELECT 
	                B.* 
                FROM ApiResource AS A 
                INNER JOIN ApiResourceScope AS B ON A.Id = B.ApiResourceId 
                WHERE A.Name = @name AND A.Enabled = 1;
                ";

                var reader = await connection.QueryMultipleAsync(sqlStr, new { name });

                var entityApiResource = reader.Read<Entities.ApiResource>();
                var entityApiResourceScope = reader.Read<Entities.ApiResourceScope>();
                if (entityApiResource != null && entityApiResource.AsList()?.Count > 0)
                {
                    var apiResource = entityApiResource.AsList()[0];
                    apiResource.Scopes = entityApiResourceScope.AsList();
                    if (apiResource != null)
                    {
                        _logger.LogDebug("Found {api} API resource in database", name);
                    }
                    else
                    {
                        _logger.LogDebug("Not found {api} API resource in database", name);
                    }
                    result = apiResource.ToModel();
                }
            }

            return result;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var result = new List<ApiResource>();

            var scopes = "";

            foreach (var scope in scopeNames)
            {
                scopes += "'" + scope + "',";
            }

            if (scopes == "")
            {
                return null;
            }
            else
            {
                scopes = scopes.Substring(0, scopes.Length - 1);
            }

            var sqlStr = $@"
            SELECT 
	            DISTINCT A.* 
            FROM ApiResource AS A
            INNER JOIN ApiResourceScope AS B ON A.Id = B.ApiResourceId 
            WHERE B.Name IN({scopes}) AND A.Enabled = 1;
            ";

            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var resources = (await connection.QueryAsync<Entities.ApiResource>(sqlStr))?.AsList();
                if (resources != null && resources.Count > 0)
                {
                    foreach (var resource in resources)
                    {
                        sqlStr = $@"
                        SELECT 
	                        * 
                        FROM ApiResourceScope 
                        WHERE ApiResourceId = @id
                        ";

                        var apiResourceScopes = (await connection.QueryAsync<Entities.ApiResourceScope>(sqlStr, new { id = resource.Id }))?.AsList();
                        resource.Scopes = apiResourceScopes;
                        result.Add(resource.ToModel());
                    }

                    _logger.LogDebug("Found {scopes} API scopes in database", result.SelectMany(x => x.Scopes).Select(x => x.Name));
                }
            }

            return result;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var result = new List<IdentityResource>();

            var scopes = "";

            foreach (var scope in scopeNames)
            {
                scopes += "'" + scope + "',";
            }

            if (scopes == "")
            {
                return null;
            }
            else
            {
                scopes = scopes.Substring(0, scopes.Length - 1);
            }

            //暂不实现 IdentityClaims
            var sqlStr = $@"
            SELECT 
	            * 
            FROM IdentityResource 
            WHERE Name IN({scopes}) AND Enabled = 1 
            ";

            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var resources = (await connection.QueryAsync<Entities.IdentityResource>(sqlStr))?.AsList();
                if (resources != null && resources.Count > 0)
                {
                    foreach (var resource in resources)
                    {
                        result.Add(resource.ToModel());
                    }
                }
            }

            return result;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var result = new List<ApiResource>();

            var resources = new List<IdentityResource>();

            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sqlStr = $@"
                SELECT 
	                * 
                FROM IdentityResource 
                WHERE Enabled = 1
                ";

                var identityResources = (await connection.QueryAsync<Entities.IdentityResource>(sqlStr))?.AsList();
                if (identityResources != null && identityResources.Count > 0)
                {
                    foreach (var resource in identityResources)
                    {
                        resources.Add(resource.ToModel());
                    }
                }

                sqlStr = $@"
                SELECT 
	                * 
                FROM ApiResource 
                WHERE Enabled = 1
                ";

                var apiResources = (await connection.QueryAsync<Entities.ApiResource>(sqlStr))?.AsList();
                if (apiResources != null && apiResources.Count > 0)
                {
                    foreach (var resource in apiResources)
                    {
                        sqlStr = $@"
                        SELECT 
	                        * 
                        FROM ApiResourceScope 
                        WHERE ApiResourceId = @id
                        ";

                        var apiResourceScopes = (await connection.QueryAsync<Entities.ApiResourceScope>(sqlStr, new { id = resource.Id }))?.AsList();
                        resource.Scopes = apiResourceScopes;
                        result.Add(resource.ToModel());
                    }
                }
            }

            return new Resources(resources, result);
        }
    }
}

using Dapper;
using IdentityServer4.Dapper.Mappers;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly DapperStoreOptions _dapperStoreOptions;

        public ResourceStore(DapperStoreOptions dapperStoreOptions)
        {
            _dapperStoreOptions = dapperStoreOptions;
        }

        public async Task<Models.ApiResource> FindApiResourceAsync(string name)
        {
            var result = new Models.ApiResource();
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT 
	                Id,
                    Enabled,
                    Name,
                    DisplayName,
                    Description
                FROM ApiResource
                WHERE Name = @name;
                ";
                var resource = (await connection.QueryFirstOrDefaultAsync<Entities.ApiResource>(sql, new { name }));

                if (resource == null) return null;

                sql = $@"
                SELECT 
	                Id,
                    ApiResourceId,
                    Type
                FROM ApiResourceClaim 
                WHERE ApiResourceId = @id;
                ";
                resource.ApiResourceClaims = (await connection.QueryAsync<Entities.ApiResourceClaim>(sql, new { id = resource.Id }))?.AsList();

                sql = $@"
                SELECT 
	                Id,
                    ApiResourceId,
                    K,
                    V
                FROM ApiResourceProperty 
                WHERE ApiResourceId = @id;
                ";
                resource.ApiResourceProperties = (await connection.QueryAsync<Entities.ApiResourceProperty>(sql, new { id = resource.Id }))?.AsList();

                sql = $@"
                SELECT 
	                Id,
                    ApiResourceId,
                    Name,
                    DisplayName,
                    Description,
                    Required,
                    Emphasize,
                    ShowInDiscoveryDocument
                FROM ApiResourceScope 
                WHERE ApiResourceId = @id;
                ";
                var apiResourceScopes = (await connection.QueryAsync<Entities.ApiResourceScope>(sql, new { id = resource.Id }))?.AsList();
                if (apiResourceScopes != null && apiResourceScopes.Count > 0)
                {
                    foreach (var scope in apiResourceScopes)
                    {
                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceScopeId,
                            Type
                        FROM ApiResourceScopeClaim 
                        WHERE ApiResourceScopeId = @id;
                        ";
                        scope.ApiResourceScopeClaims = (await connection.QueryAsync<Entities.ApiResourceScopeClaim>(sql, new { id = resource.Id }))?.AsList();
                    }
                }
                resource.ApiResourceScopes = apiResourceScopes;

                sql = $@"
                SELECT 
	                Id,
                    ApiResourceId,
                    Description,
                    Value,
                    Expiration,
                    Type
                FROM ApiResourceSecret 
                WHERE ApiResourceId = @id;
                ";
                resource.ApiResourceSecrets = (await connection.QueryAsync<Entities.ApiResourceSecret>(sql, new { id = resource.Id }))?.AsList();

                result = resource.ToModel();
            }
            return result;
        }

        public async Task<IEnumerable<Models.ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
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

            var result = new List<Models.ApiResource>();
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT DISTINCT 
	                A.Id,
                    A.Enabled,
                    A.Name,
                    A.DisplayName,
                    A.Description
                FROM ApiResource AS A
                INNER JOIN ApiResourceScope AS B ON A.Id = B.ApiResourceId 
                WHERE B.Name IN({scopes});
                ";
                var apiResources = (await connection.QueryAsync<Entities.ApiResource>(sql))?.AsList();
                if (apiResources != null && apiResources.Count > 0)
                {
                    foreach (var resource in apiResources)
                    {
                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            Type
                        FROM ApiResourceClaim 
                        WHERE ApiResourceId = @id;
                        ";
                        resource.ApiResourceClaims = (await connection.QueryAsync<Entities.ApiResourceClaim>(sql, new { id = resource.Id }))?.AsList();

                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            K,
                            V
                        FROM ApiResourceProperty 
                        WHERE ApiResourceId = @id;
                        ";
                        resource.ApiResourceProperties = (await connection.QueryAsync<Entities.ApiResourceProperty>(sql, new { id = resource.Id }))?.AsList();

                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            Name,
                            DisplayName,
                            Description,
                            Required,
                            Emphasize,
                            ShowInDiscoveryDocument
                        FROM ApiResourceScope 
                        WHERE ApiResourceId = @id;
                        ";
                        var apiResourceScopes = (await connection.QueryAsync<Entities.ApiResourceScope>(sql, new { id = resource.Id }))?.AsList();
                        if (apiResourceScopes != null && apiResourceScopes.Count > 0)
                        {
                            foreach (var scope in apiResourceScopes)
                            {
                                sql = $@"
                                SELECT 
	                                Id,
                                    ApiResourceScopeId,
                                    Type
                                FROM ApiResourceScopeClaim 
                                WHERE ApiResourceScopeId = @id;
                                ";
                                scope.ApiResourceScopeClaims = (await connection.QueryAsync<Entities.ApiResourceScopeClaim>(sql, new { id = resource.Id }))?.AsList();
                            }
                        }
                        resource.ApiResourceScopes = apiResourceScopes;

                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            Description,
                            Value,
                            Expiration,
                            Type
                        FROM ApiResourceSecret 
                        WHERE ApiResourceId = @id;
                        ";
                        resource.ApiResourceSecrets = (await connection.QueryAsync<Entities.ApiResourceSecret>(sql, new { id = resource.Id }))?.AsList();

                        result.Add(resource.ToModel());
                    }
                }
            }
            return result;
        }

        public async Task<IEnumerable<Models.IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
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

            var result = new List<Models.IdentityResource>();
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT 
	                Id,
	                Enabled,
                    Name,
                    DisplayName,
                    Description,
                    Required,
                    Emphasize,
                    ShowInDiscoveryDocument
                FROM IdentityResource
                WHERE Name IN({scopes});
                ";
                var identityResources = (await connection.QueryAsync<Entities.IdentityResource>(sql))?.AsList();
                if (identityResources != null && identityResources.Count > 0)
                {
                    foreach (var resource in identityResources)
                    {
                        sql = $@"
                        SELECT 
	                        Id,
                            IdentityResourceId,
                            Type
                        FROM IdentityResourceClaim 
                        WHERE IdentityResourceId = @id;
                        ";
                        resource.IdentityClaims = (await connection.QueryAsync<Entities.IdentityResourceClaim>(sql, new { id = resource.Id }))?.AsList();

                        sql = $@"
                        SELECT 
	                        Id,
                            IdentityResourceId,
                            K,
                            V
                        FROM IdentityResourceProperty 
                        WHERE IdentityResourceId = @id;
                        ";
                        resource.IdentityResourceProperties = (await connection.QueryAsync<Entities.IdentityResourceProperty>(sql, new { id = resource.Id }))?.AsList();

                        result.Add(resource.ToModel());
                    }
                }
            }
            return result;
        }

        public async Task<Models.Resources> GetAllResourcesAsync()
        {
            var identities = new List<Models.IdentityResource>();
            var apis = new List<Models.ApiResource>();
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT 
	                Id,
	                Enabled,
                    Name,
                    DisplayName,
                    Description,
                    Required,
                    Emphasize,
                    ShowInDiscoveryDocument
                FROM IdentityResource;
                ";
                var identityResources = (await connection.QueryAsync<Entities.IdentityResource>(sql))?.AsList();
                if (identityResources != null && identityResources.Count > 0)
                {
                    foreach (var resource in identityResources)
                    {
                        sql = $@"
                        SELECT 
	                        Id,
                            IdentityResourceId,
                            Type
                        FROM IdentityResourceClaim 
                        WHERE IdentityResourceId = @id;
                        ";
                        resource.IdentityClaims = (await connection.QueryAsync<Entities.IdentityResourceClaim>(sql, new { id = resource.Id }))?.AsList();

                        identities.Add(resource.ToModel());
                    }
                }

                sql = $@"
                SELECT 
	                Id,
                    Enabled,
                    Name,
                    DisplayName,
                    Description
                FROM ApiResource;
                ";
                var apiResources = (await connection.QueryAsync<Entities.ApiResource>(sql))?.AsList();
                if (apiResources != null && apiResources.Count > 0)
                {
                    foreach (var resource in apiResources)
                    {
                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            Type
                        FROM ApiResourceClaim 
                        WHERE ApiResourceId = @id;
                        ";
                        resource.ApiResourceClaims = (await connection.QueryAsync<Entities.ApiResourceClaim>(sql, new { id = resource.Id }))?.AsList();

                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            K,
                            V
                        FROM ApiResourceProperty 
                        WHERE ApiResourceId = @id;
                        ";
                        resource.ApiResourceProperties = (await connection.QueryAsync<Entities.ApiResourceProperty>(sql, new { id = resource.Id }))?.AsList();

                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            Name,
                            DisplayName,
                            Description,
                            Required,
                            Emphasize,
                            ShowInDiscoveryDocument
                        FROM ApiResourceScope 
                        WHERE ApiResourceId = @id;
                        ";
                        var apiResourceScopes = (await connection.QueryAsync<Entities.ApiResourceScope>(sql, new { id = resource.Id }))?.AsList();
                        if (apiResourceScopes != null && apiResourceScopes.Count > 0)
                        {
                            foreach (var scope in apiResourceScopes)
                            {
                                sql = $@"
                                SELECT 
	                                Id,
                                    ApiResourceScopeId,
                                    Type
                                FROM ApiResourceScopeClaim 
                                WHERE ApiResourceScopeId = @id;
                                ";
                                scope.ApiResourceScopeClaims = (await connection.QueryAsync<Entities.ApiResourceScopeClaim>(sql, new { id = resource.Id }))?.AsList();
                            }
                        }
                        resource.ApiResourceScopes = apiResourceScopes;

                        sql = $@"
                        SELECT 
	                        Id,
                            ApiResourceId,
                            Description,
                            Value,
                            Expiration,
                            Type
                        FROM ApiResourceSecret 
                        WHERE ApiResourceId = @id;
                        ";
                        resource.ApiResourceSecrets = (await connection.QueryAsync<Entities.ApiResourceSecret>(sql, new { id = resource.Id }))?.AsList();

                        apis.Add(resource.ToModel());
                    }
                }
            }
            return new Models.Resources(identities, apis);
        }
    }
}

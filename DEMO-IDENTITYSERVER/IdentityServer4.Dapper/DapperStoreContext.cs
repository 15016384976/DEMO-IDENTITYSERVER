using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4.Dapper
{
    public class DapperStoreContext : DbContext
    {
        public DapperStoreContext(DbContextOptions<DapperStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApiResourceConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourcePropertyConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceScopeConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceScopeClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceSecretConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ClientClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ClientCorsOriginConfiguration());
            modelBuilder.ApplyConfiguration(new ClientGrantTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientIdPRestrictionConfiguration());
            modelBuilder.ApplyConfiguration(new ClientPostLogoutRedirectUriConfiguration());
            modelBuilder.ApplyConfiguration(new ClientPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRedirectUriConfiguration());
            modelBuilder.ApplyConfiguration(new ClientScopeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientSecretConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceFlowCodesConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourceConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourceClaimConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourcePropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PersistedGrantConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

using IdentityServer4.Dapper.Migrations.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4.Dapper.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
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

            modelBuilder.ApplyConfiguration(new DeviceCodeConfiguration());

            modelBuilder.ApplyConfiguration(new IdentityResourceConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourceClaimConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourcePropertyConfiguration());

            modelBuilder.ApplyConfiguration(new PersistedGrantConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

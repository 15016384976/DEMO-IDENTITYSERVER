using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ApiResourceScopeClaimConfiguration : IEntityTypeConfiguration<ApiResourceScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceScopeClaim> builder)
        {
            builder.ToTable("ApiResourceScopeClaim");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceScopeId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(200).IsRequired(true);
        }
    }
}

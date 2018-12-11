using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ApiResourceClaimConfiguration : IEntityTypeConfiguration<ApiResourceClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceClaim> builder)
        {
            builder.ToTable("ApiResourceClaim");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(200).IsRequired(true);
        }
    }
}

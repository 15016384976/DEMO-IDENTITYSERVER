using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class IdentityResourceClaimConfiguration : IEntityTypeConfiguration<IdentityResourceClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceClaim> builder)
        {
            builder.ToTable("IdentityResourceClaim");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.IdentityResourceId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(200).IsRequired(true);
        }
    }
}

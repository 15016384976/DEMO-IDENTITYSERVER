using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class IdentityResourcePropertyConfiguration : IEntityTypeConfiguration<IdentityResourceProperty>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceProperty> builder)
        {
            builder.ToTable("IdentityResourceProperty");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.IdentityResourceId).IsRequired(true);

            builder.Property(v => v.Key).HasMaxLength(250).IsRequired(true);
            builder.Property(v => v.Value).HasMaxLength(2000).IsRequired(true);
        }
    }
}

using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable("ApiResource");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Enabled).IsRequired(true);
            builder.Property(v => v.Name).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.DisplayName).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.Description).HasMaxLength(1000).IsRequired(false);
        }
    }
}

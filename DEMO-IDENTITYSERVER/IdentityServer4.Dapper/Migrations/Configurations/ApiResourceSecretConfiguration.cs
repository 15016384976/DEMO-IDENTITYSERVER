using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ApiResourceSecretConfiguration : IEntityTypeConfiguration<ApiResourceSecret>
    {
        public void Configure(EntityTypeBuilder<ApiResourceSecret> builder)
        {
            builder.ToTable("ApiResourceSecret");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceId).IsRequired(true);

            builder.Property(v => v.Description).HasMaxLength(2000).IsRequired(false);
            builder.Property(v => v.Value).HasMaxLength(4000).IsRequired(true);
            builder.Property(v => v.Expiration).IsRequired(false);
            builder.Property(v => v.Type).HasMaxLength(250).IsRequired(true);
        }
    }
}

using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ClientPropertyConfiguration : IEntityTypeConfiguration<ClientProperty>
    {
        public void Configure(EntityTypeBuilder<ClientProperty> builder)
        {
            builder.ToTable("ClientProperty");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.Key).HasMaxLength(250).IsRequired(true);
            builder.Property(v => v.Value).HasMaxLength(2000).IsRequired(true);
        }
    }
}

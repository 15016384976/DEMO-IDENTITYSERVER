using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ClientGrantTypeConfiguration : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> builder)
        {
            builder.ToTable("ClientGrantType");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.GrantType).HasMaxLength(250).IsRequired(true);
        }
    }
}

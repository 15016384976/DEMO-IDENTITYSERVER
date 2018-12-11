using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> builder)
        {
            builder.ToTable("ClientScope");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.Scope).HasMaxLength(200).IsRequired(true);
        }
    }
}

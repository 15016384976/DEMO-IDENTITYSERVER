using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ClientRedirectUriConfiguration : IEntityTypeConfiguration<ClientRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientRedirectUri> builder)
        {
            builder.ToTable("ClientRedirectUri");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.RedirectUri).HasMaxLength(2000).IsRequired(true);
        }
    }
}

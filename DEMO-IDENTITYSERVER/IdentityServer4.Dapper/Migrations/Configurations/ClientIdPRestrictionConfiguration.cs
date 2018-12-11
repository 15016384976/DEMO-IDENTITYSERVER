using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ClientIdPRestrictionConfiguration : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> builder)
        {
            builder.ToTable("ClientIdPRestriction");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.Provider).HasMaxLength(200).IsRequired(true);
        }
    }
}

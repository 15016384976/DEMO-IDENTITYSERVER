using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientIdPRestriction
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Provider { get; set; }
        
        public Client Client { get; set; }
    }

    public class ClientIdPRestrictionConfiguration : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> builder)
        {
            builder.ToTable(nameof(ClientIdPRestriction));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.Provider).HasMaxLength(200).IsRequired(true);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientClaim
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
        
        public Client Client { get; set; }
    }

    public class ClientClaimConfiguration : IEntityTypeConfiguration<ClientClaim>
    {
        public void Configure(EntityTypeBuilder<ClientClaim> builder)
        {
            builder.ToTable(nameof(ClientClaim));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(250).IsRequired(true);
            builder.Property(v => v.Value).HasMaxLength(250).IsRequired(true);
        }
    }
}

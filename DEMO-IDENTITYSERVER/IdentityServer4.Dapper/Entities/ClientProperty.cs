using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientProperty
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string K { get; set; }
        public string V { get; set; }

        public Client Client { get; set; }
    }

    public class ClientPropertyConfiguration : IEntityTypeConfiguration<ClientProperty>
    {
        public void Configure(EntityTypeBuilder<ClientProperty> builder)
        {
            builder.ToTable(nameof(ClientProperty));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.K).HasMaxLength(250).IsRequired(true);
            builder.Property(v => v.V).HasMaxLength(2000).IsRequired(true);
        }
    }
}

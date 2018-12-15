using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientCorsOrigin
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Origin { get; set; }
        
        public Client Client { get; set; }
    }

    public class ClientCorsOriginConfiguration : IEntityTypeConfiguration<ClientCorsOrigin>
    {
        public void Configure(EntityTypeBuilder<ClientCorsOrigin> builder)
        {
            builder.ToTable(nameof(ClientCorsOrigin));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.Origin).HasMaxLength(150).IsRequired(true);
        }
    }
}

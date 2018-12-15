using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string RedirectUri { get; set; }
        
        public Client Client { get; set; }
    }

    public class ClientRedirectUriConfiguration : IEntityTypeConfiguration<ClientRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientRedirectUri> builder)
        {
            builder.ToTable(nameof(ClientRedirectUri));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ClientId).IsRequired(true);

            builder.Property(v => v.RedirectUri).HasMaxLength(2000).IsRequired(true);
        }
    }
}

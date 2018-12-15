using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class IdentityResourceClaim
    {
        public int Id { get; set; }
        public int IdentityResourceId { get; set; }

        public string Type { get; set; }

        public IdentityResource IdentityResource { get; set; }
    }

    public class IdentityResourceClaimConfiguration : IEntityTypeConfiguration<IdentityResourceClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceClaim> builder)
        {
            builder.ToTable(nameof(IdentityResourceClaim));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.IdentityResourceId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(200).IsRequired(true);
        }
    }
}

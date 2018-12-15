using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class IdentityResourceProperty
    {
        public int Id { get; set; }
        public int IdentityResourceId { get; set; }

        public string K { get; set; }
        public string V { get; set; }
        
        public IdentityResource IdentityResource { get; set; }
    }

    public class IdentityResourcePropertyConfiguration : IEntityTypeConfiguration<IdentityResourceProperty>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceProperty> builder)
        {
            builder.ToTable(nameof(IdentityResourceProperty));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.IdentityResourceId).IsRequired(true);

            builder.Property(v => v.K).HasMaxLength(250).IsRequired(true);
            builder.Property(v => v.V).HasMaxLength(2000).IsRequired(true);
        }
    }
}

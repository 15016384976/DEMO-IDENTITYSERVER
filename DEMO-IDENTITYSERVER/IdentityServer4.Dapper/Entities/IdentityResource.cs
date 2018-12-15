using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Entities
{
    public class IdentityResource
    {
        public int Id { get; set; }

        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;

        public List<IdentityResourceClaim> IdentityClaims { get; set; }
        public List<IdentityResourceProperty> IdentityResourceProperties { get; set; }
    }

    public class IdentityResourceConfiguration : IEntityTypeConfiguration<IdentityResource>
    {
        public void Configure(EntityTypeBuilder<IdentityResource> builder)
        {
            builder.ToTable(nameof(IdentityResource));

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Enabled).IsRequired(true);
            builder.Property(v => v.Name).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.DisplayName).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.Description).HasMaxLength(1000).IsRequired(false);

            builder.Property(v => v.Required).IsRequired(true);
            builder.Property(v => v.Emphasize).IsRequired(true);
            builder.Property(v => v.ShowInDiscoveryDocument).IsRequired(true);

            builder.HasIndex(v => v.Name).IsUnique();

            builder.HasMany(v => v.IdentityClaims).WithOne(v => v.IdentityResource).HasForeignKey(v => v.IdentityResourceId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.IdentityResourceProperties).WithOne(v => v.IdentityResource).HasForeignKey(v => v.IdentityResourceId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

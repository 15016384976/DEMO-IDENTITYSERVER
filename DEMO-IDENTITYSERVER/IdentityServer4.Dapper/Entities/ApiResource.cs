using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResource
    {
        public int Id { get; set; }

        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<ApiResourceClaim> ApiResourceClaims { get; set; }
        public List<ApiResourceProperty> ApiResourceProperties { get; set; }
        public List<ApiResourceScope> ApiResourceScopes { get; set; }
        public List<ApiResourceSecret> ApiResourceSecrets { get; set; }
    }

    public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable(nameof(ApiResource));

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Enabled).IsRequired(true);
            builder.Property(v => v.Name).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.DisplayName).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.Description).HasMaxLength(1000).IsRequired(false);

            builder.HasIndex(v => v.Name).IsUnique(true);

            builder.HasMany(v => v.ApiResourceClaims).WithOne(v => v.ApiResource).HasForeignKey(v => v.ApiResourceId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ApiResourceProperties).WithOne(v => v.ApiResource).HasForeignKey(v => v.ApiResourceId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ApiResourceScopes).WithOne(v => v.ApiResource).HasForeignKey(v => v.ApiResourceId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ApiResourceSecrets).WithOne(v => v.ApiResource).HasForeignKey(v => v.ApiResourceId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

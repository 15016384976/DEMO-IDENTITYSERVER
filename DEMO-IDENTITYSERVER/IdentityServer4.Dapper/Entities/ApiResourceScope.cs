using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceScope
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;

        public List<ApiResourceScopeClaim> ApiResourceScopeClaims { get; set; }

        public ApiResource ApiResource { get; set; }
    }

    public class ApiResourceScopeConfiguration : IEntityTypeConfiguration<ApiResourceScope>
    {
        public void Configure(EntityTypeBuilder<ApiResourceScope> builder)
        {
            builder.ToTable(nameof(ApiResourceScope));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceId).IsRequired(true);

            builder.Property(v => v.Name).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.DisplayName).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(v => v.Required).IsRequired(true);
            builder.Property(v => v.Emphasize).IsRequired(true);
            builder.Property(v => v.ShowInDiscoveryDocument).IsRequired(true);

            builder.HasIndex(v => v.Name).IsUnique(true);

            builder.HasMany(v => v.ApiResourceScopeClaims).WithOne(v => v.ApiResourceScope).HasForeignKey(v => v.ApiResourceScopeId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

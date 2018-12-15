using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceScopeClaim
    {
        public int Id { get; set; }
        public int ApiResourceScopeId { get; set; }

        public string Type { get; set; }

        public ApiResourceScope ApiResourceScope { get; set; }
    }

    public class ApiResourceScopeClaimConfiguration : IEntityTypeConfiguration<ApiResourceScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceScopeClaim> builder)
        {
            builder.ToTable(nameof(ApiResourceScopeClaim));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceScopeId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(200).IsRequired(true);
        }
    }
}

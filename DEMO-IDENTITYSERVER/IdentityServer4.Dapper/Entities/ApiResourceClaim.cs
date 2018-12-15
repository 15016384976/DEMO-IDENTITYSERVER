using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceClaim
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Type { get; set; }

        public ApiResource ApiResource { get; set; }
    }

    public class ApiResourceClaimConfiguration : IEntityTypeConfiguration<ApiResourceClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceClaim> builder)
        {
            builder.ToTable(nameof(ApiResourceClaim));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceId).IsRequired(true);

            builder.Property(v => v.Type).HasMaxLength(200).IsRequired(true);
        }
    }
}

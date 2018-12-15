using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceProperty
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string K { get; set; }
        public string V { get; set; }

        public ApiResource ApiResource { get; set; }
    }

    public class ApiResourcePropertyConfiguration : IEntityTypeConfiguration<ApiResourceProperty>
    {
        public void Configure(EntityTypeBuilder<ApiResourceProperty> builder)
        {
            builder.ToTable(nameof(ApiResourceProperty));

            builder.HasKey(v => v.Id);
            builder.Property(v => v.ApiResourceId).IsRequired(true);

            builder.Property(v => v.K).HasMaxLength(250).IsRequired(true);
            builder.Property(v => v.V).HasMaxLength(2000).IsRequired(true);
        }
    }
}

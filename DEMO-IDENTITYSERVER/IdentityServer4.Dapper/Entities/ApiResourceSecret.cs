using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceSecret
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = "SharedSecret";

        public ApiResource ApiResource { get; set; }
    }

    public class ApiResourceSecretConfiguration : IEntityTypeConfiguration<ApiResourceSecret>
    {
        public void Configure(EntityTypeBuilder<ApiResourceSecret> builder)
        {
            builder.ToTable(nameof(ApiResourceSecret));

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(v => v.Value).HasMaxLength(4000).IsRequired(true);
            builder.Property(v => v.Expiration).IsRequired(false);
            builder.Property(v => v.Type).HasMaxLength(250).IsRequired(true);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IdentityServer4.Dapper.Entities
{
    public class PersistedGrant
    {
        public string K { get; set; }
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }

    public class PersistedGrantConfiguration : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> builder)
        {
            builder.ToTable(nameof(PersistedGrant));

            builder.HasKey(x => x.K);

            builder.Property(v => v.K).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.Type).HasMaxLength(50).IsRequired(true);
            builder.Property(v => v.SubjectId).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.ClientId).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.CreationTime).IsRequired(true);
            builder.Property(v => v.Expiration).IsRequired(false);
            builder.Property(v => v.Data).HasMaxLength(50000).IsRequired(true);

            builder.HasIndex(v => new { v.SubjectId, v.ClientId, v.Type });
        }
    }
}

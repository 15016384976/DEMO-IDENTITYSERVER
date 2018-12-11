using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class PersistedGrantConfiguration : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> builder)
        {
            builder.ToTable("PersistedGrant");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Key).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.Type).HasMaxLength(50).IsRequired(true);
            builder.Property(v => v.SubjectId).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.ClientId).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.CreationTime).IsRequired(true);
            builder.Property(v => v.Expiration).IsRequired(false);
            builder.Property(v => v.Data).IsRequired(true);
        }
    }
}

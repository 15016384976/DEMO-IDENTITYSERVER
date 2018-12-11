using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class DeviceCodeConfiguration : IEntityTypeConfiguration<DeviceCode>
    {
        public void Configure(EntityTypeBuilder<DeviceCode> builder)
        {
            builder.ToTable("DeviceCode");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Code).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.UserCode).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.SubjectId).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.ClientId).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.CreationTime).IsRequired(true);
            builder.Property(v => v.Expiration).IsRequired(true);
            builder.Property(v => v.Data).IsRequired(true);
        }
    }
}

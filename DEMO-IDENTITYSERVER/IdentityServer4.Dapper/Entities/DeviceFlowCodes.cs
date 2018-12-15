using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IdentityServer4.Dapper.Entities
{
    public class DeviceFlowCodes
    {
        public string DeviceCode { get; set; }

        public string UserCode { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }

    public class DeviceFlowCodesConfiguration : IEntityTypeConfiguration<DeviceFlowCodes>
    {
        public void Configure(EntityTypeBuilder<DeviceFlowCodes> builder)
        {
            builder.ToTable(nameof(DeviceFlowCodes));

            builder.HasKey(v => v.DeviceCode);

            builder.Property(v => v.DeviceCode).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.UserCode).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.SubjectId).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.ClientId).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.CreationTime).IsRequired(true);
            builder.Property(v => v.Expiration).IsRequired(true);
            builder.Property(v => v.Data).HasMaxLength(50000).IsRequired(true);

            builder.HasIndex(v => v.DeviceCode).IsUnique();
        }
    }
}

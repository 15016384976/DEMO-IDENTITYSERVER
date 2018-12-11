using IdentityServer4.Dapper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.Dapper.Migrations.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Enabled).IsRequired(true);
            builder.Property(v => v.ClientId).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.ProtocolType).HasMaxLength(200).IsRequired(true);
            builder.Property(v => v.RequireClientSecret).IsRequired(true);
            builder.Property(v => v.ClientName).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(v => v.ClientUri).HasMaxLength(2000).IsRequired(false);
            builder.Property(v => v.LogoUri).HasMaxLength(2000).IsRequired(false);
            builder.Property(v => v.RequireConsent).IsRequired(true);
            builder.Property(v => v.AllowRememberConsent).IsRequired(true);
            builder.Property(v => v.AlwaysIncludeUserClaimsInIdToken).IsRequired(true);
            builder.Property(v => v.RequirePkce).IsRequired(true);
            builder.Property(v => v.AllowPlainTextPkce).IsRequired(true);
            builder.Property(v => v.AllowAccessTokensViaBrowser).IsRequired(true);
            builder.Property(v => v.FrontChannelLogoutUri).HasMaxLength(2000).IsRequired(false);
            builder.Property(v => v.FrontChannelLogoutSessionRequired).IsRequired(true);
            builder.Property(v => v.BackChannelLogoutUri).HasMaxLength(2000).IsRequired(false);
            builder.Property(v => v.BackChannelLogoutSessionRequired).IsRequired(true);
            builder.Property(v => v.AllowOfflineAccess).IsRequired(true);
            builder.Property(v => v.IdentityTokenLifetime).IsRequired(true);
            builder.Property(v => v.AccessTokenLifetime).IsRequired(true);
            builder.Property(v => v.AuthorizationCodeLifetime).IsRequired(true);
            builder.Property(v => v.ConsentLifetime).IsRequired(false);
            builder.Property(v => v.AbsoluteRefreshTokenLifetime).IsRequired(true);
            builder.Property(v => v.SlidingRefreshTokenLifetime).IsRequired(true);
            builder.Property(v => v.RefreshTokenUsage).IsRequired(true);
            builder.Property(v => v.UpdateAccessTokenClaimsOnRefresh).IsRequired(true);
            builder.Property(v => v.RefreshTokenExpiration).IsRequired(true);
            builder.Property(v => v.AccessTokenType).IsRequired(true);
            builder.Property(v => v.EnableLocalLogin).IsRequired(true);
            builder.Property(v => v.IncludeJwtId).IsRequired(true);
            builder.Property(v => v.AlwaysSendClientClaims).IsRequired(true);
            builder.Property(v => v.ClientClaimsPrefix).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.PairWiseSubjectSalt).HasMaxLength(200).IsRequired(false);
            builder.Property(v => v.UserSsoLifetime).IsRequired(false);
            builder.Property(v => v.UserCodeType).HasMaxLength(100).IsRequired(false);
            builder.Property(v => v.DeviceCodeLifetime).IsRequired(true);
        }
    }
}

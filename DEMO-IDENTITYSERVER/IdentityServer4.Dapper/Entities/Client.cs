using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace IdentityServer4.Dapper.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string ClientId { get; set; }
        public string ProtocolType { get; set; } = "oidc";
        public bool RequireClientSecret { get; set; } = true;
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; } = true;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; }
        public int IdentityTokenLifetime { get; set; } = 300;
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int? ConsentLifetime { get; set; } = null;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public int RefreshTokenUsage { get; set; } = (int)TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; } = (int)TokenExpiration.Absolute;
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; } = "client_";
        public string PairWiseSubjectSalt { get; set; }
        public int? UserSsoLifetime { get; set; }
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; } = 300;

        public List<ClientClaim> ClientClaims { get; set; }
        public List<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public List<ClientGrantType> ClientGrantTypes { get; set; }
        public List<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        public List<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public List<ClientProperty> ClientProperties { get; set; }
        public List<ClientRedirectUri> ClientRedirectUris { get; set; }
        public List<ClientScope> ClientScopes { get; set; }
        public List<ClientSecret> ClientSecrets { get; set; }
    }

    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client));

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
            builder.Property(x => x.FrontChannelLogoutUri).HasMaxLength(2000);
            builder.Property(v => v.FrontChannelLogoutSessionRequired).IsRequired(true);
            builder.Property(x => x.BackChannelLogoutUri).HasMaxLength(2000);
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

            builder.HasIndex(v => v.ClientId).IsUnique();

            builder.HasMany(v => v.ClientClaims).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientCorsOrigins).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientGrantTypes).WithOne(v => v.Client).HasForeignKey(v=> v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientIdPRestrictions).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientPostLogoutRedirectUris).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientProperties).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientRedirectUris).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientScopes).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(v => v.ClientSecrets).WithOne(v => v.Client).HasForeignKey(v => v.ClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

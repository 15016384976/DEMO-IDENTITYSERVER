using IdentityServer4.Dapper.HostedServices;
using IdentityServer4.Dapper.Stores;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace IdentityServer4.Dapper.Extensions
{
    public static class IdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddDapperStore(this IIdentityServerBuilder builder, Action<DapperStoreOptions> options = null)
        {
            var opts = new DapperStoreOptions();

            builder.Services.AddSingleton(opts);
            options?.Invoke(opts);

            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            builder.Services.AddTransient<IPersistedGrantExpiredCleanup, PersistedGrantExpiredCleanup>();
            builder.Services.AddSingleton<TokenCleanup>();
            builder.Services.AddSingleton<IHostedService, TokenCleanupHostedService>();

            return builder;
        }
    }
}

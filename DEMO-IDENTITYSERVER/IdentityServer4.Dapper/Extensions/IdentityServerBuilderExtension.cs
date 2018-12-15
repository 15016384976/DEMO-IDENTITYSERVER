using IdentityServer4.Dapper.Services;
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

            builder.AddClientStore<ClientStore>();
            builder.AddResourceStore<ResourceStore>();
            builder.AddCorsPolicyService<CorsPolicyService>();

            builder.AddInMemoryCaching();
            builder.AddClientStoreCache<ClientStore>();
            builder.AddResourceStoreCache<ResourceStore>();
            builder.AddCorsPolicyCache<CorsPolicyService>();

            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            builder.Services.AddTransient<IDeviceFlowStore, DeviceFlowStore>();

            builder.Services.AddSingleton<TokenCleanup>();
            builder.Services.AddSingleton<IHostedService, TokenCleanupHostedService>();

            return builder;
        }
    }
}

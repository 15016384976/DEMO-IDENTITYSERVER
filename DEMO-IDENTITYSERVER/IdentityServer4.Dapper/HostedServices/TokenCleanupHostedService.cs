using IdentityServer4.Dapper.Stores;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.HostedServices
{
    public class TokenCleanupHostedService : IHostedService
    {
        private readonly TokenCleanup _tokenCleanup;
        private readonly DapperStoreOptions _dapperStoreOptions;

        public TokenCleanupHostedService(TokenCleanup tokenCleanup, DapperStoreOptions dapperStoreOptions)
        {
            _tokenCleanup = tokenCleanup;
            _dapperStoreOptions = dapperStoreOptions;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_dapperStoreOptions.TokenCleanupEnabled)
            {
                _tokenCleanup.Start(cancellationToken);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_dapperStoreOptions.TokenCleanupEnabled)
            {
                _tokenCleanup.Stop();
            }
            return Task.CompletedTask;
        }
    }

    public class TokenCleanup
    {
        private readonly ILogger<TokenCleanup> _logger;
        private readonly DapperStoreOptions _dapperStoreOptions;
        private readonly IPersistedGrantExpiredCleanup _persistedGrantExpiredCleanup;
        private CancellationTokenSource _cancellationTokenSource;

        public TimeSpan TokenCleanupInterval => TimeSpan.FromSeconds(_dapperStoreOptions.TokenCleanupInterval);

        public TokenCleanup(ILogger<TokenCleanup> logger, DapperStoreOptions dapperStoreOptions, IPersistedGrantExpiredCleanup persistedGrantExpiredCleanup)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dapperStoreOptions = dapperStoreOptions ?? throw new ArgumentNullException(nameof(dapperStoreOptions));
            if (_dapperStoreOptions.TokenCleanupInterval < 1) throw new ArgumentException("Token cleanup interval must be at least 1 second");
            _persistedGrantExpiredCleanup = persistedGrantExpiredCleanup;
        }

        public void Start()
        {
            Start(CancellationToken.None);
        }

        public void Start(CancellationToken cancellationToken)
        {
            if (_cancellationTokenSource != null) throw new InvalidOperationException("Already started. Call Stop first.");

            _logger.LogDebug("Starting token cleanup");

            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            Task.Factory.StartNew(() => StartInternal(_cancellationTokenSource.Token));
        }

        public void Stop()
        {
            if (_cancellationTokenSource == null) throw new InvalidOperationException("Not started. Call Start first.");

            _logger.LogDebug("Stopping token cleanup");

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
        }

        private async Task StartInternal(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogDebug("CancellationRequested. Exiting.");
                    break;
                }

                try
                {
                    await Task.Delay(TokenCleanupInterval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    _logger.LogDebug("TaskCanceledException. Exiting.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Task.Delay exception: {0}. Exiting.", ex.Message);
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogDebug("CancellationRequested. Exiting.");
                    break;
                }

                ClearTokens();
            }
        }

        public void ClearTokens()
        {
            try
            {
                _logger.LogTrace("Querying for tokens to clear");

                _persistedGrantExpiredCleanup.Cleanup(DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception clearing tokens: {exception}", ex.Message);
            }
        }
    }
}

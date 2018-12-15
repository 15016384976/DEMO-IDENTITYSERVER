using Dapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Services
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
        private readonly DapperStoreOptions _dapperStoreOptions;

        private CancellationTokenSource _cancellationTokenSource;

        private TimeSpan CleanupInterval => TimeSpan.FromSeconds(_dapperStoreOptions.TokenCleanupInterval);

        public TokenCleanup(DapperStoreOptions dapperStoreOptions)
        {
            _dapperStoreOptions = dapperStoreOptions;
        }

        public void Start(CancellationToken cancellationToken)
        {
            if (_cancellationTokenSource == null)
            {
                _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }
                        try
                        {
                            await Task.Delay(CleanupInterval, cancellationToken);
                        }
                        catch (TaskCanceledException)
                        {
                            break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }
                        using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
                        {
                            var sql = $@"
                            DELETE 
                            FROM PersistedGrant 
                            WHERE Expiration < @dateTime";
                            var i = await connection.ExecuteAsync(sql, new { dateTime = DateTime.Now });
                        }
                    }
                });
            }
        }

        public void Stop()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
            }
        }
    }
}

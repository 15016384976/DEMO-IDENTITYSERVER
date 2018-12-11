using IdentityServer4.Dapper.Extensions;
using IdentityServer4.Dapper.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DEMO_IDENTITYSERVER
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=identityserver;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddDapperStore(options =>
                    {
                        options.DbConnectionString = connectionString;
                        options.TokenCleanupEnabled = false;
                        options.TokenCleanupInterval = 3600;
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}

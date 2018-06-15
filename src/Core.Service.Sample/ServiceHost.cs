using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Service.Sample
{
    public class ApplicationLifetimeHostedService : IHostedService
    {
        IApplicationLifetime appLifetime;
        ILogger<ApplicationLifetimeHostedService> logger;
        IHostingEnvironment environment;
        IConfiguration configuration;
        public ApplicationLifetimeHostedService(
            IConfiguration configuration,
            IHostingEnvironment environment,
            ILogger<ApplicationLifetimeHostedService> logger, 
            IApplicationLifetime appLifetime)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.environment = environment;

            //Log.Logger = new LoggerConfiguration()
            //                  .ReadFrom.Configuration(configuration)
            //                  .CreateLogger();

        }




        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("StartAsync method called.");

            this.appLifetime.ApplicationStarted.Register(OnStarted);
            this.appLifetime.ApplicationStopping.Register(OnStopping);
            this.appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;

        }

        private void OnStarted()
        {
            this.logger.LogInformation("OnStarted method called.");

            // Post-startup code goes here
        }

        private void OnStopping()
        {
            this.logger.LogInformation("OnStopping method called.");

            // On-stopping code goes here
        }

        private void OnStopped()
        {
            this.logger.LogInformation("OnStopped method called.");

            // Post-stopped code goes here
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("StopAsync method called.");

            return Task.CompletedTask;
        }
    }
}

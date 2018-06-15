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

            Log.Logger = new LoggerConfiguration()
                              .ReadFrom.Configuration(configuration)
                              .CreateLogger();

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        MethodBase GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod();
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{GetCurrentMethod().Name} method called.");

            this.appLifetime.ApplicationStarted.Register(OnStarted);
            this.appLifetime.ApplicationStopping.Register(OnStopping);
            this.appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;

        }

        private void OnStarted()
        {
            this.logger.LogInformation($"{GetCurrentMethod().Name} method called.");

            // Post-startup code goes here
        }

        private void OnStopping()
        {
            this.logger.LogInformation($"{GetCurrentMethod().Name} method called.");

            // On-stopping code goes here
        }

        private void OnStopped()
        {
            this.logger.LogInformation($"{GetCurrentMethod().Name} method called.");

            // Post-stopped code goes here
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{GetCurrentMethod().Name} method called.");

            return Task.CompletedTask;
        }
    }
}

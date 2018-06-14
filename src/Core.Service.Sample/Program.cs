using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Service.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IHost host = new HostBuilder()
                 .ConfigureHostConfiguration(configHost =>
                 {
                     configHost.SetBasePath(Directory.GetCurrentDirectory());
                     configHost.AddJsonFile("appsettings.json", optional: true);
                     configHost.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                     configHost.AddCommandLine(args);
                 })
                 .ConfigureAppConfiguration((hostContext, configApp) =>
                 {
                     configApp.AddJsonFile("appsettings.json", optional: true);
                     configApp.AddJsonFile(
                         $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                         optional: true);
                     configApp.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                     configApp.AddCommandLine(args);
                 })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ServiceHost>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {

                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .Build();

            await host.RunAsync();
        }


    }
}

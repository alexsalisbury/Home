namespace Home.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Extensions.Logging;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.Models.Settings;
    using Home.Core.DiscordBot.Services;
    using Home.Core.DiscordBot.Interfaces.Services;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?tabs=windows&view=aspnetcore-5.0#secret-manager"/>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.host.createdefaultbuilder?view=dotnet-plat-ext-5.0
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var logConfig = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostContext.Configuration);

                    Log.Logger = logConfig.CreateLogger();
                    services.AddHostedService<Orchestrator>();
                    services.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(Log.Logger, false));
                })
                .UseWindowsService()
                .UseSerilog();
    }
}

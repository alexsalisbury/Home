namespace Home.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.Models.Settings;

    public class Program
    {
        private static string envName;

        public static async Task Main(string[] args)
        {
            var configuration = LoadConfig();
            SetupLogging(configuration);

            //Check statement for debugging use.
            Worker.AzureSettings = configuration.GetSection("Azure").Get<AzureSettings>();
            Worker.ShyBotSettings = configuration.GetSection("BotSettings").Get<BotSettings>();
            await Task.Delay(1);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });

        private static IConfiguration LoadConfig()
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-5.0
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>(true);

            foreach (var s in GetConfigFiles())
            {
                try
                {
                    builder = builder.AddJsonFile(s, true);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
            }

            return builder.Build();
        }

        private static IEnumerable<string> GetConfigFiles()
        {
            var envName = GetEnvironmentName();
            switch (envName.ToLower())
            {
                case "development":
                    yield return "appsettings.Development.json";
                    yield return "Home.Service.Dev.json";
                    break;
                case "test":
                    yield return "appsettings.Test.json";
                    break;
                case "local":
                    yield return "appsettings.Local.json";
                    yield return "Home.Service.Local.json";
                    break;
                case "azure":
                    yield return "appsettings.Azure.json";
                    break;
                default:
                    break;
            }
        }

        private static string GetEnvironmentName()
        {
            if (string.IsNullOrWhiteSpace(envName))
            {
                var env = Environment.GetEnvironmentVariable("HOME_SERVICE_ENV");
                envName = env ?? envName;
            }

            return envName ?? string.Empty;
        }

        private static void SetupLogging(IConfiguration configuration)
        {
            var logConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            Log.Logger = logConfig.CreateLogger();

            //TODO: Log relevant settings on startup.
            Log.Warning("Starting.");
            Log.Information("Environment.Machinename: {machineName}", Environment.MachineName);
            Log.Information("Environment Name: {env}", GetEnvironmentName());
        }

        //private static IConfiguration LoadConfig()
        //{
        //    // TODO: Config by Environment. Read from A: Drive?
        //    var builder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .AddUserSecrets<Program>();

        //    return builder.Build();
        //}

        //private static void SetupLogging(IConfiguration configuration)
        //{
        //    Log.Logger = new LoggerConfiguration()
        //        .ReadFrom.Configuration(configuration)
        //        .CreateLogger();

        //    //TODO: Log relevant settings on startup.
        //    Log.Information("Home.Service on {host} is online.", Environment.MachineName);
        //}

    }
}

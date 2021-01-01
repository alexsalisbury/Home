namespace Shy.Cloud
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Program
    {
        private static string envName;

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Init");
            var configuration = LoadConfig();
            Console.WriteLine("Code load.");
            SetupLogging(configuration);
            await Task.Delay(1000);

            Console.WriteLine("Starting");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(Log.Logger)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json");

                    foreach (var s in GetConfigFiles())
                    {
                        builder = builder.AddJsonFile(s);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static IConfiguration LoadConfig()
        {
            // TODO: Config by Environment. Read from A: Drive?
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-5.0
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            foreach (var s in GetConfigFiles())
            {
                try
                {
                    builder = builder.AddJsonFile(s);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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
                    yield return "ShyCloud.Dev.json";
                    break;
                case "test":
                    yield return "appsettings.Test.json";
                    break;
                case "local":
                    yield return "appsettings.Local.json";
                    yield return "ShyCloud.Local.json";
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
                var env = Environment.GetEnvironmentVariable("SHY_CLOUD_ENV");
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
    }
}

namespace Shy.Cloud
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = LoadConfig();
            SetupLogging(configuration);
            await Task.Delay(1000);

            CreateHostBuilder(args).Build().Run();
        }

        private static IConfiguration LoadConfig()
        {
            // TODO: Config by Environment. Read from A: Drive?
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-5.0
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var envName = GetEnvironmentName();
            switch (envName.ToLower())
            {
                case "development":
                    builder = builder.AddJsonFile("appsettings.Development.json");
                    break;
                case "test":
                    builder = builder.AddJsonFile("appsettings.Test.json");
                    break;
                case "local":
                    builder = builder.AddJsonFile("appsettings.Local.json");
                    break;
                case "azure":
                    builder = builder.AddJsonFile("appsettings.Azure.json");
                    break;
                default:
                    break;
            }

            builder.AddUserSecrets<Program>();
            return builder.Build();
        }

        private static string GetEnvironmentName()
        {
            var env = Environment.GetEnvironmentVariable("SHY_CLOUD_ENV");
            return env ?? string.Empty;
        }

        private static void SetupLogging(IConfiguration configuration)
        {
            var logConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            var k = logConfig.CreateLogger();
            var m = logConfig.WriteTo;
            Log.Logger = k;
            //TODO: Log relevant settings on startup.
            Log.Warning("Starting.");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(Log.Logger)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

namespace Shy.Cloud
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
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
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>();

            return builder.Build();
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

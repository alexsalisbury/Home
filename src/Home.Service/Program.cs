namespace Home.Service
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Home.Core.Models.Settings;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = LoadConfig();
            SetupLogging(configuration);

            //Check statement for debugging use.
            Worker.AzureSettings = configuration.GetSection("Azure").Get<AzureSettings>();
         //   Worker.ShyBotSettings = configuration.GetSection("BotSettings").Get<BotSettings>();
            await Task.Delay(1);

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
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            //TODO: Log relevant settings on startup.
            Log.Information("Online.");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}

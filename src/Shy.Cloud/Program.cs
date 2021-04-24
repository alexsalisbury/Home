namespace Shy.Cloud
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class Program
    {
        private static string envName;

        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json");

                    foreach (var s in GetConfigFiles())
                    {
                        try
                        {
                            Log.Information("Adding file {configFile} to host", s);
                            builder = builder.AddJsonFile(s);
                        }
                        catch (Exception ex)
                        {
                           Log.Error(ex.ToString());
                        }
                    }
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseSerilog();


        private static IConfiguration LoadConfig()
        {
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

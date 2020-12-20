namespace Home.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Services;
    using Home.Core.Models.Settings;
    using Home.Core.DiscordBot.Clients;

    public class Worker : BackgroundService
    {
        /// <summary>
        /// Global settings from Configuration.
        /// </summary>
        public static BotSettings ShyBotSettings { get; internal set; }
        public static AzureSettings AzureSettings { get; internal set; }
        private static IList<ServerInfo> EmptyServerSet = new List<ServerInfo>(new List<ServerInfo>());
        private DiscordService DiscordManager;

        public Worker()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartupAsync();
            await Task.Delay(1000 * (10 - (DateTime.UtcNow.Second%10)));
            Log.Information("Worker running at: {time}", DateTimeOffset.UtcNow);

            int c = DateTime.UtcNow.Minute / 10;
            while (!stoppingToken.IsCancellationRequested)
            {
                c = (c + 1) % 6;
                if (c == 0)
                {
                    Log.Information("Worker running at: {time}", DateTimeOffset.UtcNow);
                }

                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task StartupAsync()
        {
            var cloud = new ShyCloudClient(AzureSettings);
            await DiscordService.StartAsync(ShyBotSettings, cloud);

            while (!DiscordService.IsConnected())
            {
                await Task.Delay(100);
            }

            await Task.Delay(2000);
            Log.Information("Go");

            DiscordManager = DiscordService.Service;

            Log.Information("Running tasks");
            await QueueStartupTasksAsync();
        }

        private async Task QueueStartupTasksAsync()
        {
            await DiscordManager.FetchConfigurationAsync();
            await DiscordManager.SyncInfraAsync();
            await DiscordManager.StartArchiveAsync();
        }

        private async Task QueueWaitingTasksAsync()
        {
            await Task.CompletedTask;
            // await DiscordManager.ArchiveAsync();
        }
    }
}
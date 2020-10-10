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

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task StartupAsync()
        {
            await DiscordService.StartAsync(ShyBotSettings);

            while (!DiscordService.IsConnected())
            {
                await Task.Delay(100);
            }

            DiscordManager = new DiscordService(ShyBotSettings);

            await RunStartupTasksAsync();
        }

        private async Task RunStartupTasksAsync()
        {
            //await Task.CompletedTask;
            await DiscordManager.ArchiveAsync();
        }
    }
}
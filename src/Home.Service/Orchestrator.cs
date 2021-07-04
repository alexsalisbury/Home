namespace Home.Service
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Interfaces.Services;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Services;
    using Home.Core.Models.Settings;
    using Home.Core.Workers;
    using Microsoft.Extensions.Configuration;

    public class Orchestrator : BackgroundService
    {
        private readonly ILoggerFactory factory;
        private readonly ILogger<Orchestrator> logger;
        private List<HomeWorker> workers = new List<HomeWorker>();
        private IConfiguration configuration;

        public Orchestrator(IConfiguration config, ILoggerFactory factory)
        {
            this.configuration = config;
            this.logger = this.logger ?? factory.CreateLogger<Orchestrator>();
            this.logger?.LogInformation("Orchestrator:Ctor");
            this.factory = factory;
        }

        public async Task InitializeAsync()
        {
            logger?.LogInformation("Orchestrator:Initialize");

            if (this.workers.Any())
            {
                logger?.LogWarning("Orchestrator:Zombies");
                Debugger.Break();
            }

            var d = new DiscordAdminWorker(0, factory.CreateLogger<DiscordAdminWorker>());
            DiscordAdminWorker.ShyBotSettings = configuration.GetSection("ShyBot").Get<BotSettings>();
            workers.Add(d);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger?.LogInformation("Orchestrator:ExecuteAsync");
                await InitializeAsync();
                logger?.LogInformation("Orchestrator:TaskLaunch");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Orchestrator:Failure.");
            }
        }
    }
}
namespace Home.Core.DiscordBot.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Serilog;
    using Home.Core.Clients;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.Interfaces.Settings;
    using Home.Core.Services;
    using Home.Core.Interfaces;

    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-netcore-daemon
    /// </summary>
    public class ShyCloudClient : HomeClient
    {
        public ShyCloudClient(IAzureSettings settings, HttpMessageHandler handler = null) : base(settings, new ClientSecretTokenService(settings), handler)
        {
        }

        public ShyCloudClient(IAzureSettings settings, IAcquireTokenService tokenService, HttpMessageHandler handler = null) : base(settings, tokenService, handler)
        {
        }

        public async Task<IEnumerable<IExplainable>> FetchExplainablesAsync()
        {
            var url = settings.ShyCloudEndpoint + "explain";
            using (var client = await GetClientAsync(settings))
            {
                try
                {
                    var result = await client.GetAsync(url);
                    result.EnsureSuccessStatusCode();
                    var content = await result.Content.ReadAsStringAsync();
                    var set = JsonConvert.DeserializeObject<List<ExplainableDto>>(content);
                    return set;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to fetch Explainables");
                }
            }

            return new List<ExplainableDto>();
        }

        public async Task UploadChannelAsync(IChannelInfo dto)
        {
            var url = (settings.ShyCloudEndpoint) + "channel";
            using (var client = await GetClientAsync(settings))
            {
                try
                {
                    var result = await client.PostAsJsonAsync(url, dto);
                    result.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to fetch Explainables");
                    throw ex;
                }
            }
        }
    }
}

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
    using Home.Core.Models.Settings;
    using Home.Core.Services;

    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-netcore-daemon
    /// </summary>
    public class ShyCloudClient : HomeClient
    {
        public ShyCloudClient(AzureSettings settings) : base(settings, new ClientSecretTokenService(settings))
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
            var url = settings.ShyCloudEndpoint + "channel";
            using (HttpClient client = HttpClientFactory.Create())
            {
                try
                {
                    var result = await client.PostAsJsonAsync(url, dto);
                    result.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to fetch Explainables");
                }
            }
        }
    }
}

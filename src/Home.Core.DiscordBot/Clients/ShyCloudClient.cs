namespace Home.Core.DiscordBot.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;
    using Newtonsoft.Json;
    using Serilog;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.Models.Settings;

    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-v2-netcore-daemon
    /// </summary>
    public class ShyCloudClient
    {
        private AzureSettings settings;

        public ShyCloudClient(AzureSettings settings)
        {
            this.settings = settings;
        }

        private static async Task<HttpClient> GetClientAsync(AzureSettings settings)
        {
            HttpClient client = HttpClientFactory.Create();
            var (scheme, parameter) = await GetHeaderAsync(settings, null);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return client;
        }

        private static async Task<(string, string)> GetHeaderAsync(AzureSettings settings, X509Certificate2? cert)
        {
            //cert = cert ?? ReadCert();

            var scopes = new List<string>();
            scopes.Add(settings.Scope);

            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create(settings.ClientId)
                .WithClientSecret(settings.ClientSecret)
                // .WithCertificate(cert)
                .WithAuthority(new Uri(settings.Authority))
                .Build();

            var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

            var header = result.CreateAuthorizationHeader().Split(' ');
            return (header[0], header[1]);
        }

        private void Test()
        {
            //X509Certificate2 certificate = ReadCertificate(config.CertificateName);
            //var app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
            //    .WithCertificate(certificate)
            //    .WithAuthority(new Uri(config.Authority))
            //    .Build();
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

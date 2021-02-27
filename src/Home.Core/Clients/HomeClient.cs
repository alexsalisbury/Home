namespace Home.Core.Clients
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Home.Core.Models.Settings;
    using Home.Core.Interfaces;

    public abstract class HomeClient
    {
        protected AzureSettings settings;
        protected IAcquireTokenService tokenService;

        protected HomeClient(AzureSettings settings, IAcquireTokenService tokenService)
        {
            this.settings = settings;
            this.tokenService = tokenService;
        }

        protected async Task<HttpClient> GetClientAsync(AzureSettings settings)
        {
            HttpClient client = HttpClientFactory.Create();
            var (scheme, parameter) = await GetHeaderAsync();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return client;
        }

        protected async Task<(string, string)> GetHeaderAsync()
        {
            var result = await tokenService.GetTokenHeader();
            var header = result.Split(' ');
            return (header[0], header[1]);
        }
    }
}

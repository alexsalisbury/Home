namespace Home.Core.Clients
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Home.Core.Interfaces;
    using Home.Core.Interfaces.Settings;

    public abstract class HomeClient
    {
        protected IAzureSettings settings;
        protected IAcquireTokenService tokenService;
        protected HttpMessageHandler handler;

        protected HomeClient(IAzureSettings settings, IAcquireTokenService tokenService, HttpMessageHandler handler = null)
        {
            this.settings = settings;
            this.tokenService = tokenService;
            this.handler = handler;
        }

        protected async Task<HttpClient> GetClientAsync(IAzureSettings settings)
        {
            HttpClient client = handler == null ? HttpClientFactory.Create(): HttpClientFactory.Create(handler);
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

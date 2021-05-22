namespace Home.Core.Clients
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Home.Core.Interfaces;
    using Home.Core.Interfaces.Settings;

    /// <summary>
    /// Base for all personal, home and bot API clients. This encapsulates generating an HttpClient that's already token auth'd.
    /// </summary>
    /// <remarks>Allows for stubbing of Token acquirement and Http message handling.</remarks>
    /// <seealso cref="HomeCertClient"/>
    /// <seealso cref="ShyCloudClient"/>
    public abstract class HomeClient
    {
        /// <summary>
        /// Settings for Azure Items including Auth to AAD, Vault Info, Queue Info, etc.
        /// </summary>
        protected IAzureSettings settings;

        /// <summary>
        /// The service to fetch Bearer Tokens from.
        /// </summary>
        protected IAcquireTokenService tokenService;

        /// <summary>
        /// The HttpMessageHandler
        /// </summary>
        protected HttpMessageHandler handler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="tokenService"></param>
        /// <param name="handler"></param>
        protected HomeClient(IAzureSettings settings, IAcquireTokenService tokenService, HttpMessageHandler handler = null)
        {
            this.settings = settings;
            this.tokenService = tokenService;
            this.handler = handler;
        }

        /// <summary>
        /// Creates a Client with the authorization set to valid token (if able)
        /// </summary>
        /// <returns>HttpClient that uses customer that's authenticated using the class's tokenService member.</returns>
        protected async Task<HttpClient> GetClientAsync()
        {
            HttpClient client = handler == null ? HttpClientFactory.Create(): HttpClientFactory.Create(handler);
            var (scheme, parameter) = await GetBearerTokenHeaderAsync();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return client;
        }

        /// <summary>
        /// Gets a bearer token from the token service.
        /// </summary>
        /// <returns></returns>
        protected async Task<(string, string)> GetBearerTokenHeaderAsync()
        {
            var result = await tokenService.GetBearerTokenHeaderAsync();
            var header = result.Split(' ');
            return (header[0], header[1]);
        }
    }
}

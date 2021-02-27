namespace Home.Core.Tests.Mocks
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Home.Core.Clients;
    using Home.Core.Models.Settings;

    public class MockClient : HomeClient
    {
        public MockClient(AzureSettings settings) : base(settings, new MockTokenService())
        {

        }

        public MockClient(AzureSettings settings, MockTokenService tokenService) : base(settings, tokenService)
        {

        }

        public async Task<HttpClient> WrappedGetClientAsync()
        {
            return await this.GetClientAsync(null);
        }

        public async Task<(string, string)> WrappedGetHeaderAsync()
        {
            return await this.GetHeaderAsync();
        }
    }
}
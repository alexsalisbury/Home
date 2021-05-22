namespace Home.Core.Tests.Mocks
{
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;
    using Home.Core.Models.Settings;
    using Home.Core.Services;

    public class MockClientSecretTokenService : ClientSecretTokenService
    {
        public MockClientSecretTokenService(AzureSettings settings) : base(settings)
        {
        }

        public IConfidentialClientApplication WrapGetBuilder()
        {
            return this.GetBuilder();
        }

        public async Task<string> WrapGetTokenHeader()
        {
            return await this.GetBearerTokenHeaderAsync();
        }
    }
}
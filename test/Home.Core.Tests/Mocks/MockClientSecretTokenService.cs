namespace Home.Core.Tests.Mocks
{
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
    }
}
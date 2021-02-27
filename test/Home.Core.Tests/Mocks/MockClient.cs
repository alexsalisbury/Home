namespace Home.Core.Tests.Mocks
{
    using Home.Core.Clients;
    using Home.Core.Models.Settings;

    public class MockClient : HomeClient
    {
        public MockClient(AzureSettings settings): base(settings, new MockTokenService())
        {

        }
    }
}
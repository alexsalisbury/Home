namespace Home.Core.DiscordBot.Tests.Clients
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Xunit;
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.Tests.Mocks;
    using Home.Core.Models.Settings;

    public class HomeClient_Tests
    {
        [Fact]
        public void BasicClientTest()
        {
            var scc = new ShyCloudClient(null, null);
            Assert.NotNull(scc);
        }

        [Fact]
        public async Task FetchExplainablesNoItemsTest()
        {
            var settings = new AzureSettings()
            {
                ShyCloudEndpoint = string.Empty
            };

            var mts = new MockTokenService();
            mts.Result = "Mock Token";

            var defaultList = new List<ExplainableDto>();
            var content = JsonConvert.SerializeObject(defaultList);
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(content),
                StatusCode = HttpStatusCode.OK
            };

            var handler = new TestHttpMessageHandler();
            var scc = new ShyCloudClient(settings, mts, handler);
            Assert.NotNull(scc);

            handler.SetExpectedResponse(response);
            var actualResponse = await scc.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

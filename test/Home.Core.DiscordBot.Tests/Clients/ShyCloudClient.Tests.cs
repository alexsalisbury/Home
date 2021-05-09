namespace Home.Core.DiscordBot.Tests.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Xunit;
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.DiscordBot.Repositories;
    using Home.Core.Interfaces.Models;
    using Home.Core.Models.Settings;
    using Home.Core.Tests.Mocks;

    public partial class ShyCloudClient_Tests
    {
        [Fact]
        public void BasicClientTest()
        {
            var scc = new ShyCloudClient(null, null);
            Assert.NotNull(scc);
        }

        [Fact]
        public void PopulatedClientTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);
            Assert.NotNull(scc);
        }

        private static (AzureSettings, MockTokenService, TestHttpMessageHandler) GetDefaultParams()
        {
            var settings = new AzureSettings()
            {
                ShyCloudEndpoint = "https://localhost/",
            };

            var mts = new MockTokenService();
            mts.Result = "Mock Token";

            var handler = new TestHttpMessageHandler();
            return (settings, mts, handler);
        }

        private static HttpResponseMessage MakeSimpleResponse(HttpStatusCode code)
        {
            return new HttpResponseMessage()
            {
                StatusCode = code
            };
        }

        private static HttpResponseMessage MakeInternalErrorResponse()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("Mocked exception."),
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        private static HttpResponseMessage MakeContentResponse(HttpStatusCode code, string jsonContent)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(jsonContent),
                StatusCode = code
            };
        }
    }
}

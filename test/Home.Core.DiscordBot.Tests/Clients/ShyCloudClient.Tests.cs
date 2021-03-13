﻿namespace Home.Core.DiscordBot.Tests.Clients
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Xunit;
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Interfaces.Models;
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
        public async Task PopulatedClientTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);
            Assert.NotNull(scc);
        }

        [Fact]
        public async Task FetchExplainablesNoItemsTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            HttpResponseMessage response = MakeExplainableResponse(HttpStatusCode.OK);
            handler.SetExpectedResponse(response);

            var actualResponse = await scc?.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var con = await response.Content.ReadAsStringAsync();
            Assert.Equal(2, con.Length); // preflight
            Assert.Equal("[]", con);
        }

        [Fact]
        public async Task FetchExplainablesOneItemTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            List<ExplainableDto> result = GetResultSet(1);
            HttpResponseMessage response = MakeExplainableResponse(HttpStatusCode.OK, result);
            handler.SetExpectedResponse(response);

            var actualResponse = await scc?.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var con = await response.Content.ReadAsStringAsync();
            Assert.Equal(52, con.Length); // preflight
            Assert.Equal("[{\"ShyId\":1,\"Subject\":\"Only\",\"Explanation\":\"First\"}]", con);
        }

        [Fact]
        public async Task FetchExplainablesTwoItemsTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            List<ExplainableDto> result = GetResultSet(2);
            HttpResponseMessage response = MakeExplainableResponse(HttpStatusCode.OK, result);
            handler.SetExpectedResponse(response);

            var actualResponse = await scc?.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var con = await response.Content.ReadAsStringAsync();
            Assert.Equal(104, con.Length); // only check.
        }

        private static HttpResponseMessage MakeExplainableResponse(HttpStatusCode code, List<ExplainableDto> resultList = null)
        {
            var content = JsonConvert.SerializeObject(resultList ?? IExplainable.EmptyList);

            return new HttpResponseMessage()
            {
                Content = new StringContent(content),
                StatusCode = code
            };
        }

        private static (AzureSettings, MockTokenService, TestHttpMessageHandler) GetDefaultParams()
        {
            var settings = new AzureSettings()
            {
                ShyCloudEndpoint = string.Empty
            };

            var mts = new MockTokenService();
            mts.Result = "Mock Token";

            var handler = new TestHttpMessageHandler();
            return (settings, mts, handler);
        }

        private List<ExplainableDto> GetResultSet(int count)
        {
            var set = new List<ExplainableDto>();

            switch (count)
            {
                case 1:
                    set.Add(new ExplainableDto() { ShyId = 1, Explanation = "First", Subject = "Only" });
                    break;
                case 2:
                    set.Add(new ExplainableDto() { ShyId = 2, Explanation = "Second", Subject = "Twin" });
                    set.Add(new ExplainableDto() { ShyId = 3, Explanation = "Third", Subject = "Twin" });
                    break;
                default:  
                    set.Clear();
                    break;
            }

            return set;
        }
    }
}

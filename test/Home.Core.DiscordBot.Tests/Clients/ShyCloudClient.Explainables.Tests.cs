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
    using Home.Core.DiscordBot.Repositories;
    using Home.Core.Interfaces.Models;
    using Home.Core.Models.Settings;
    using Home.Core.Tests.Mocks;

    public partial class ShyCloudClient_Tests
    {
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

            var result = GetResultSet(1);
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

            var result = GetResultSet(2);
            HttpResponseMessage response = MakeExplainableResponse(HttpStatusCode.OK, result);
            handler.SetExpectedResponse(response);

            var actualResponse = await scc?.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var con = await response.Content.ReadAsStringAsync();
            Assert.Equal(104, con.Length); // only check.
        }

        [Fact]
        public async Task FetchExplainablesDefaultsTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            HttpResponseMessage response = MakeExplainableResponse(HttpStatusCode.OK, ExplainRepository.DefaultCommands);
            handler.SetExpectedResponse(response);

            var actualResponse = await scc?.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var conjson = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<ExplainableDto>>(conjson);
            Assert.Equal(5, content.Count);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task FetchExplainablesFailTest(HttpStatusCode code)
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            HttpResponseMessage response = MakeExplainableResponse(code);
            handler.SetExpectedResponse(response);

            var actualResponse = await scc?.FetchExplainablesAsync();

            Assert.NotNull(actualResponse);
            Assert.Equal(code, response.StatusCode);

            var con = await response.Content.ReadAsStringAsync();
            Assert.Equal(2, con.Length); // preflight
            Assert.Equal("[]", con);
        }

        private static HttpResponseMessage MakeExplainableResponse(HttpStatusCode code, List<ExplainableDto> resultList = null)
        {
            var content = JsonConvert.SerializeObject(resultList ?? IShyEntity.EmptyList);
            return MakeContentResponse(code, content);
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

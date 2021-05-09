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
        public async Task UploadChannelDefaultsTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            HttpResponseMessage response = MakeSimpleResponse(HttpStatusCode.OK);
            handler.SetExpectedResponse(response);

            await scc?.UploadChannelAsync(null);
        }

        [Fact]
        public async Task UploadChannelErrorTest()
        {
            var (settings, mts, handler) = GetDefaultParams();
            var scc = new ShyCloudClient(settings, mts, handler);

            HttpResponseMessage response = MakeInternalErrorResponse();
            handler.SetExpectedResponse(response);

            await Assert.ThrowsAnyAsync<Exception>(() => scc?.UploadChannelAsync(null));
        }
    }
}

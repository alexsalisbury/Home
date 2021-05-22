﻿namespace Home.Core.Tests.Mocks
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Home.Core.Clients;
    using Home.Core.Interfaces;
    using Home.Core.Interfaces.Settings;

    public class MockClient : HomeClient
    {
        public MockClient(IAzureSettings settings) : base(settings, new MockTokenService(), new TestHttpMessageHandler())
        {

        }

        public MockClient(IAzureSettings settings, IAcquireTokenService tokenService, HttpMessageHandler handler = null) : base(settings, tokenService, handler)
        {

        }

        public async Task<HttpClient> WrappedGetClientAsync()
        {
            return await this.GetClientAsync();
        }

        public async Task<(string, string)> WrappedGetHeaderAsync()
        {
            return await this.GetBearerTokenHeaderAsync();
        }
    }
}
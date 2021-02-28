namespace Home.Core.Tests.Mocks
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class TestHttpMessageHandler : HttpMessageHandler
    {
        internal HttpResponseMessage ExpectedReponse { get; set; }

        public void SetExpectedResponse(HttpResponseMessage expectedReponse)
        {
            this.ExpectedReponse = expectedReponse;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            ExpectedReponse.RequestMessage = request;
            return ExpectedReponse;
        }
    }
}
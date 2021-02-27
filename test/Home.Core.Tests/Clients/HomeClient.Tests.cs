namespace Home.Core.Tests.Clients
{
    using Xunit;
    using Home.Core.Tests.Mocks;

    public class HomeClient_Tests
    {
        [Fact]
        public void BasicClientTest()
        {
            var mc = new MockClient(null);
            Assert.NotNull(mc);
        }
    }
}

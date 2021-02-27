namespace Home.Core.Tests.Clients
{
    using System.Threading.Tasks;
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

        [Fact]
        public async Task GetClientTestAsync()
        {
            var first = "Basic";
            var second = "Auth";
            var mts = new MockTokenService();
            mts.Result = $"{first} {second}";

            var mc = new MockClient(null, mts);
            Assert.NotNull(mc);
            var h = await mc.WrappedGetClientAsync();
            Assert.NotNull(h);
        }

        [Fact]
        public async Task GetHeaderTestAsync()
        {
            var first = "Basic";
            var second = "Auth";
            var mts = new MockTokenService();
            mts.Result = $"{first} {second}";

            var mc = new MockClient(null, mts);
            Assert.NotNull(mc);
            var (firstActual, secondActual) = await mc.WrappedGetHeaderAsync();
            Assert.Equal(first, firstActual);
            Assert.Equal(second, secondActual);
        }
    }
}

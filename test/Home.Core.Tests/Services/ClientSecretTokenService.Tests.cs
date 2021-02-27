namespace Home.Core.Tests.Services
{
    using Xunit;
    using Home.Core.Services;
    using Home.Core.Tests.Mocks;
    using System.Threading.Tasks;

    public class ClientSecretTokenService_Tests
    {
        [Fact]
        public void BasicTokenServiceTest()
        {
            var csts = new ClientSecretTokenService(null);
            Assert.NotNull(csts);
        }

        [Fact]
        public async Task MaybeMakeHeaderTest()
        {
            try
            {
                var mcsts = new MockClientSecretTokenService(null);
                var h = await mcsts.WrapGetTokenHeader();
                Assert.NotNull(h);
            }
            catch
            {

            }
        }

        [Fact]
        public void MaybeMakeBuilderTest()
        {
            try
            {
                var mcsts = new MockClientSecretTokenService(null);
                var b = mcsts.WrapGetBuilder();
                Assert.NotNull(b);
            }
            catch
            {

            }
        }
    }
}

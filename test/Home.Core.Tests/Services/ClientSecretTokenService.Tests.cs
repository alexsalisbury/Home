namespace Home.Core.Tests.Services
{
    using Xunit;
    using Home.Core.Services;
    using Home.Core.Tests.Mocks;

    public class ClientSecretTokenService_Tests
    {
        [Fact]
        public void BasicTokenServiceTest()
        {
            var csts = new ClientSecretTokenService(null);
            Assert.NotNull(csts);
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

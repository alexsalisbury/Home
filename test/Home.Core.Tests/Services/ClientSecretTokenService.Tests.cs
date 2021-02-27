namespace Home.Core.Tests.Services
{
    using Xunit;
    using Home.Core.Services;

    public class ClientSecretTokenService_Tests
    {
        [Fact]
        public void BasicTokenServiceTest()
        {
            var csts = new ClientSecretTokenService(null);
            Assert.NotNull(csts);
        }
    }
}

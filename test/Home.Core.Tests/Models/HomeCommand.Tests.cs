namespace Home.Core.Tests
{
    using Xunit;
    using Home.Core.Tests.Mocks;

    public class HomeCommand_Tests
    {
        [Fact]
        public void TestHomeCommand()
        {
            var tc = new TestCommand();
            Assert.NotNull(tc);
        }
    }
}

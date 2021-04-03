namespace Home.Core.DiscordBot.Tests.Commands
{
    using Xunit;
    using Home.Core.DiscordBot.Commands;

    public class ArchiveCommand_Tests
    {
        [Fact]
        public void BasicArchiveCommand()
        {
            var ac = new ArchiveCommand(string.Empty);
            Assert.NotNull(ac);
        }
    }
}

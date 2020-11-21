namespace Home.Core.DiscordBot.Models.Dtos
{
    using Home.Core.DiscordBot.Interfaces.Models;

    public record ExplainableDto : IExplainable
    {
        public int ShyId { get; init; }
        public string Subject { get; init; }
        public string Explanation { get; init; }
    }
}
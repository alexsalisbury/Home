namespace Home.Core.DiscordBot.Interfaces.Models
{
    using Home.Core.Interfaces.Models;

    public interface IExplainable : IShyEntity
    {
        string Subject { get; }
        string Explanation { get; }
    }
}

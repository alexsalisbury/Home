namespace Home.Core.DiscordBot.Interfaces.Models
{
    using System.Collections.Generic;

    public interface IExplainable
    {
        public static IEnumerable<IExplainable> EmptyList => EmptyListCache;
        private static IEnumerable<IExplainable> EmptyListCache = new List<IExplainable>();

        int ShyId { get; }
        string Subject { get; }
        string Explanation { get; }
    }
}

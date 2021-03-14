namespace Home.Core.DiscordBot.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.DiscordBot.Models.Dtos;

    /// <summary>
    /// A Repository for explanations of commands and stuff.
    /// </summary>
    public class ExplainRepository : IExplainRepository
    {
        // This is temporary. This set will move to test code when this is no longer from static list.
        public static List<ExplainableDto> DefaultCommands = new List<ExplainableDto>();

        static ExplainRepository()
        {
            if (DefaultCommands.Count == 0)
            {
                DefaultCommands.Add(new ExplainableDto()
                {
                    ShyId = 1,
                    Subject = "explain",
                    Explanation = "The explain command will explain stuff to you. It's cool. Use '!explain list' to get current commands."
                });

                DefaultCommands.Add(new ExplainableDto()
                {
                    ShyId = 2,
                    Subject = "list",
                    Explanation = "Known commands: 'explain', 'list'. That's it. Ok, yes, some secret commands exist. That's all I can say. Sorry. I promise. 😖"
                });

                DefaultCommands.Add(new ExplainableDto()
                {
                    ShyId = 1001,
                    Subject = "yourself",
                    Explanation = "You're not my supervisor!"
                });

                DefaultCommands.Add(new ExplainableDto()
                {
                    ShyId = 21001,
                    Subject = "democracy",
                    Explanation = "The democracy module acts as a virtual congress. Work is in progress."
                });

                DefaultCommands.Add(new ExplainableDto()
                {
                    ShyId = 21002,
                    Subject = "checkquorum",
                    Explanation = "This tool checks whether a quorum exists for a specific group. Work is in progress."
                });
            }
        }

        public Task<IQueryable<IExplainable>> Fetch()
        {
            return Task.FromResult(DefaultCommands.AsQueryable<IExplainable>());
        }

        public Task<IExplainable> Fetch(ulong id)
        {
            return Task.FromResult(DefaultCommands.FirstOrDefault<IExplainable>(c => c.ShyId == (int)id));
        }

        public Task<IExplainable> Fetch(int id)
        {
            return Task.FromResult(DefaultCommands.FirstOrDefault<IExplainable>(c => c.ShyId == (int)id));
        }
    }
}

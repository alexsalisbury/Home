namespace Shy.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Shy.Core.Models.Dtos;

    /// <summary>
    /// A Repository for explanations of commands and stuff.
    /// </summary>
    public class ExplainRepository
    {
        public static IList<ExplainableDto> Commands = new List<ExplainableDto>();

        static ExplainRepository()
        {
            if (Commands.Count == 0)
            {
                Commands.Add(new ExplainableDto()
                {
                    Id = 1,
                    Subject = "explain",
                    Explanation = "The explain command will explain stuff to you. It's cool. Use '!explain list' to get current commands."
                });

                Commands.Add(new ExplainableDto()
                {
                    Id = 2,
                    Subject = "list",
                    Explanation = "Known commands: 'explain', 'list'. That's it. Ok, yes, some secret commands exist. That's all I can say. Sorry. I promise. 😖"
                });

                Commands.Add(new ExplainableDto()
                {
                    Id = 1001,
                    Subject = "yourself",
                    Explanation = "You're not my supervisor!"
                });

                Commands.Add(new ExplainableDto()
                {
                    Id = 21001,
                    Subject = "democracy",
                    Explanation = "The democracy module acts as a virtual congress. Work is in progress."
                });

                Commands.Add(new ExplainableDto()
                {
                    Id = 21002,
                    Subject = "checkquorum",
                    Explanation = "This tool checks whether a quorum exists for a specific group. Work is in progress."
                });
            }
        }

        public static Task<IQueryable<ExplainableDto>> Fetch()
        {
            return Task.FromResult(Commands.AsQueryable());
        }

    //    public Task<IQueryable<ExplainableDto>> Fetch()
    //    {
    //        return Task.FromResult(Commands.AsQueryable());
    //    }

    //    public Task<ExplainableDto> Fetch(int id)
    //    {
    //        return Task.FromResult(Commands.FirstOrDefault(c => c.Id == id));
    //    }

    //    public Task<ExplainableDto> Fetch(ulong id)
    //    {
    //        return Task.FromResult(Commands.FirstOrDefault(c => c.Id == (int)id));
    //    }
    }
}

namespace ShyCloud.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.DiscordBot.Interfaces.Models;

    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChannelController : ControllerBase
    {
        private IChannelRepository repo;
        public ChannelController(IChannelRepository executionRepository)
        {
            this.repo = executionRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await repo.Fetch();
            Log.Information("Getting {value} Channels.", value.Count());
            return new JsonResult(value);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var value = await repo.Fetch(id);
            var findstatus = value == null ? "unfound" : "found";
            Log.Information("Getting {findstatus} Channel {channelId}.", findstatus, id);
            return new JsonResult(value);
        }

        // [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(IChannelInfo channel)
        {
            var value = await repo.EnsureAsync(channel);
            if (value)
            {
                return Ok();
            }
            else
            {
                return this.Problem();
            }
        }
    }
}
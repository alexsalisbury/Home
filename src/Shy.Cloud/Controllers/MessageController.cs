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
    public class MessageController : ControllerBase
    {
        private IMessageRepository repo;
        public MessageController(IMessageRepository executionRepository)
        {
            this.repo = executionRepository;
        }

        // [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(IMessageInfo message)
        {
            var value = await repo.EnsureAsync(message);
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
//namespace ShyCloud.Controllers
//{
//    using System.Linq;
//    using System.Threading.Tasks;
//    using Microsoft.AspNetCore.Authorization;
//    using Microsoft.AspNetCore.Mvc;
//    using Serilog;
//    using Home.Core.DiscordBot.Interfaces.Repositories;
//    using Home.Core.DiscordBot.Interfaces.Models;

//    //[Authorize]
//    [ApiController]
//    [Route("[controller]")]
//    public class UserController : ControllerBase
//    {
//        private IUserRepository repo;
//        public UserController(IUserRepository executionRepository)
//        {
//            this.repo = executionRepository;
//        }

//        [AllowAnonymous]
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var value = await repo.Fetch();
//            Log.Information("Getting {value} Users.", value.Count());
//            return new JsonResult(value);
//        }

//        [AllowAnonymous]
//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(ulong id)
//        {
//            var value = await repo.Fetch(id);
//            var findstatus = value == null ? "unfound" : "found";
//            Log.Information("Getting {findstatus} User {UserId}.", findstatus, id);
//            return new JsonResult(value);
//        }

//        // [AllowAnonymous]
//        [HttpPost]
//        public async Task<IActionResult> Post(IUserInfo User)
//        {
//            var value = await repo.EnsureAsync(User);
//            if (value)
//            {
//                return Ok();
//            }
//            else
//            {
//                return this.Problem();
//            }
//        }
//    }
//}
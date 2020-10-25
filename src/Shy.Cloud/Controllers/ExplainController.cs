namespace ShyCloud.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;
    using Shy.Core.Repositories;

    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExplainController : ControllerBase
    {
        //IExplainRepository repo;
        public ExplainController()//IExplainRepository executionRepository)
        {
            //this.repo = executionRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var value = await ExplainRepository.Fetch();
            Log.Information($"Getting {value.Count()} Explains.");
            return new JsonResult(value);
        }
    }
}

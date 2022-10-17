using Microsoft.AspNetCore.Mvc;

namespace API.Version.Sample.Controller
{
    [ApiController]
    [ApiVersion("1.0")] //YOU CAN ADD MULTIPLE API VERSION, ADD ANOTHER [ApiVersion("x")] ATTRIBUTE ON CONTROLLER CLASS
    [ApiVersion("2.0")] //YOU CAN ADD MULTIPLE API VERSION, ADD ANOTHER [ApiVersion("x")] ATTRIBUTE ON CONTROLLER CLASS
    [Route("v{version:apiVersion}/[controller]")]
    public class SampleController : ControllerBase
    {
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> SampleAction()
        {
            await Task.Delay(10);
            return Ok();
        }

        [HttpGet("")]
        [MapToApiVersion("2.0")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> SampleAction([FromQuery]int version)
        {
            await Task.Delay(10);
            return Ok($"Sample action version {version}");
        }

        [HttpPost("")]
        [MapToApiVersion("2.0")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> SamplePostAction([FromBody] int version)
        {
            await Task.Delay(10);
            return Ok($"Sample post action version {version}");
        }
    }
}

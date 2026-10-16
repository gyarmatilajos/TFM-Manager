using Microsoft.AspNetCore.Mvc;

namespace TFM_Manager.Api.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("exception")]
        public IActionResult TestException()
        {
            throw new InvalidOperationException(
             "Ez egy szándékosan előidézett tesztkivétel.");
        }
    }
}

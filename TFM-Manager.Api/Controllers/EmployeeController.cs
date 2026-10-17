using Microsoft.AspNetCore.Mvc;

namespace TFM_Manager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

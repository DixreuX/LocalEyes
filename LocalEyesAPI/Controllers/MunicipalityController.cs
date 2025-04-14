using Microsoft.AspNetCore.Mvc;

namespace LocalEyesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MunicipalityController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

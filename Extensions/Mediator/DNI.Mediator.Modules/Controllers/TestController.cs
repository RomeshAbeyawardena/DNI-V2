using Microsoft.AspNetCore.Mvc;

namespace DNI.Mediator.Modules.Controllers
{
    [Route("{controller}/{action}")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok("Test");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Core.Controllers
{
    [Route("{controller}/{action}}")]
    public class TestController : ControllerBase
    {
        public ActionResult Index()
        {
            return Ok("Test");
        }
    }
}

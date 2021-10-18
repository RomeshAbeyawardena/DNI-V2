using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Shared.Base
{
    [Route("api/[version]/{controller}")]
    [ApiVersion("1.0.0")]
    public abstract class ApiVersionControllerBase : ControllerBase
    {
        
    }

    [Route("api/{controller}")]
    public abstract class ApiControllerBase : ControllerBase
    {

    }
}

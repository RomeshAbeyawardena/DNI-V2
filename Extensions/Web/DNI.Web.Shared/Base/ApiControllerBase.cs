using Microsoft.AspNetCore.Mvc;

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

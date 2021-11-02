﻿using Microsoft.AspNetCore.Mvc;

namespace DNI.Web.Shared.Base
{
    [Route("api/[version]/{controller}")]
    [ApiController, ApiVersion("1.0.0")]
    public abstract class ApiVersionControllerBase : ControllerBase
    {

    }

    [ApiController, Route("api/{controller}")]
    public abstract class ApiControllerBase : ControllerBase
    {

    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Shared.Abstractions
{
    public interface IWebModuleOptions
    {
        Action<IWebHostBuilder> ConfigureWebHost { get; }
        Action<MvcOptions> ConfigureMvcOptions { get; set; }
    }
}

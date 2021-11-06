using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace DNI.Cms.Modules
{
    public partial class CmsModule
    {
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Options);
        }
    }
}

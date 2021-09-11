using DNI.Extensions;
using DNI.ModuleLoader.Core;
using DNI.ModuleLoader.Core.Modules;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Sandbox
{
    public class SandboxAppModuleLoader : AppModuleLoaderBase<SandboxAppModuleLoader>
    {
        public SandboxAppModuleLoader(
            ILogger<SandboxAppModuleLoader> logger, IServiceProvider serviceProvider) 
            : base(logger, serviceProvider)
        {
        }

        public override void RegisterServices(
            IGlobalAppModuleCache<SandboxAppModuleLoader> appModuleCache, 
            IServiceCollection services)
        {
            appModuleCache.RegisterModule<BuiltinAppModule>();
        }
    }
}

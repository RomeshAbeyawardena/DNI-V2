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
            ILogger<SandboxAppModuleLoader> logger, 
            ISerializerFactory serializer, 
            IFileProvider fileProvider, 
            IAppModuleCache<SandboxAppModuleLoader> appModules) 
            : base(logger, serializer, fileProvider, appModules)
        {
        }

        public override void RegisterServices(
            IAppModuleCache<SandboxAppModuleLoader> appModuleCache, 
            IServiceCollection services)
        {
            appModuleCache.RegisterModule<BuiltinAppModule>();
        }
    }
}

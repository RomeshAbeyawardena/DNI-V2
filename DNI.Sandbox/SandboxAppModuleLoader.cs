﻿using DNI.ModuleLoader.Core;
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
    public class SandboxAppModuleLoader : AppModuleLoaderBase
    {
        public SandboxAppModuleLoader(ILogger<SandboxAppModuleLoader> logger, ISerializerFactory serializer, IFileProvider fileProvider) : base(logger, serializer, fileProvider)
        {
        }

        public override void RegisterServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}

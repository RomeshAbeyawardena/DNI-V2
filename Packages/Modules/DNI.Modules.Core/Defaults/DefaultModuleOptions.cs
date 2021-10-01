﻿using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleOptions : IModuleOptions
    {
        public DefaultModuleOptions(IModuleAssemblyOptions moduleAssembliesOptions)
        {
            ModuleAssembliesOptions = moduleAssembliesOptions;
        }

        public IModuleAssemblyOptions ModuleAssembliesOptions { get; }
    }
}

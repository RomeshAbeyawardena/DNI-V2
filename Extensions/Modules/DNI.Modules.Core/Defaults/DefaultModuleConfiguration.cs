using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Defaults;
using System;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleConfiguration : IModuleConfiguration
    {
        public bool Enabled { get; set; }
        public string ModuleName { get; set; }
        public string AssemblyName { get; set; }
        public string FileName { get; set; }
        public DefaultAssemblyOptions Options { get; set; }
        IAssemblyOptions IModuleConfiguration.Options { get => Options; set => throw new NotImplementedException(); }
    }
}

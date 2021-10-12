using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleConfiguration
    {
        bool Enabled { get; set; }
        string ModuleName { get; set; }
        string AssemblyName { get; set; }
        string FileName { get; set; }
        IAssemblyOptions Options { get; set; }
    }
}

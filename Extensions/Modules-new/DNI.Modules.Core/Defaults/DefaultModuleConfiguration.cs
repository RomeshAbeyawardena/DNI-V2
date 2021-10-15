using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleConfiguration : IModuleConfiguration
    {
        public DefaultModuleConfiguration()
        {

        }

        public IEnumerable<Type> ModuleTypes { get; set; }

        public ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider)
        {
            var activatedModuleList = new List<IModule>();
            foreach (var moduleType in ModuleTypes)
            {
                activatedModuleList.Add(serviceProvider.Activate<IModule>(moduleType));
            }

            return new DefaultCompiledModuleConfiguration { Modules = activatedModuleList };
        }
    }
}

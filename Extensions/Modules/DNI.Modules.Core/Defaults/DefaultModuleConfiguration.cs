using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleConfiguration : IModuleConfiguration
    {
        private Dictionary<Type, object> options;
#pragma warning disable IDE0052 // Remove unread private members
        private IEnumerable<IDisposable> disposables;
#pragma warning restore IDE0052 // Remove unread private members

        public IEnumerable<Type> ModuleTypes { get; set; }

        public IDictionary<Type, object> Options => options;

        public ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider)
        {
            var activatedModuleList = new List<IModule>();
            foreach (var moduleType in ModuleTypes)
            {
                activatedModuleList.Add(serviceProvider.Activate<IModule>(moduleType, out disposables));
            }

            return new DefaultCompiledModuleConfiguration(options) { Modules = activatedModuleList };
        }
    }
}

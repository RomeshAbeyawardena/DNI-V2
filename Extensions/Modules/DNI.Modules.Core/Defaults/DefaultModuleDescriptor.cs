using DNI.Core;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public static class ModuleDescriptor
    {
        public static IModuleDescriptor Create<T>(string usage, Guid? identifier = null)
        {
            return Create(typeof(T), usage, identifier);
        }

        public static IModuleDescriptor Create(Type type, string usage, Guid? identifier = null)
        {
            return new DefaultModuleDescriptor(type, usage, identifier);
        }
    }

    public class DefaultModuleDescriptor : IModuleDescriptor
    {
        public DefaultModuleDescriptor(Type type, string usage, Guid? identifier = null)
        {
            Type = type;
            Usage = usage;
            Id = identifier ?? Guid.NewGuid();
        }

        public string Usage { get; }

        public Guid Id { get; }

        public Type Type { get; }

        public IKeyValuePair<Guid, Type> ToKeyValuePair()
        {
            return DefaultKeyValuePair.Create(Id, Type);
        }
    }
}

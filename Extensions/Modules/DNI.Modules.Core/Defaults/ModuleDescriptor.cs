using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Modules.Core.Defaults
{
    public static class ModuleDescriptor
    {
        public const string DefaultUsage = "default";
        public static IModuleDescriptor Create<T>(string usage, Guid? identifier = null)
        {
            return Create(typeof(T), usage, identifier);
        }

        public static IModuleDescriptor Create(Type type, string usage, Guid? identifier = null)
        {
            return new DefaultModuleDescriptor(type, usage, identifier);
        }
    }
}

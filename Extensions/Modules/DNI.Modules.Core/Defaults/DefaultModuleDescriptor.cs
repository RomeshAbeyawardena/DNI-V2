using DNI.Core;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class DefaultModuleDescriptor : IModuleDescriptor
    {
        public DefaultModuleDescriptor(Type type, string usage, Guid? identifier = null)
        {
            Type = type;
            Usage = usage;

            if (identifier.HasValue)
            {
                if (identifier == Guid.Empty)
                {
                    throw new ArgumentException("Identifer must not be an empty guid");
                }

                Id = identifier.Value;
            }
            else
                Id = Guid.NewGuid();
        }

        public string Usage { get; }

        public Guid Id { get; }

        public Type Type { get; }

        public IKeyValuePair<Guid, Type> ToKeyValuePair()
        {
            return DefaultKeyValuePair.Create(Id, Type);
        }

        public bool Equals(IModuleDescriptor moduleDescriptor)
        {
            return moduleDescriptor.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IModuleDescriptor);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Usage, Id, Type);
        }

        public override string ToString()
        {
            return $"Identifier: {Id},Usage :{Usage},Type: {Type}";
        }
    }
}

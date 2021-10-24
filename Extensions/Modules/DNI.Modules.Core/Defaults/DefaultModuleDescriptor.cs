using DNI.Core;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using System;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleDescriptor : IModuleDescriptor
    {
        public DefaultModuleDescriptor(Type type, string usage, Guid? identifier = null, 
            bool enabled = true)
        {
            Type = type;
            Usage = usage;
            Enabled = enabled;

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

        public bool Enabled { get; }

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
            var hashCode = HashCode.Combine(Usage, Id, Type);
            Console.WriteLine("Hashcode of {0}: {1}", this, hashCode);
            return hashCode;
        }

        public override string ToString()
        {
            return $"Identifier: {Id},Usage :{Usage},Type: {Type}";
        }
    }
}

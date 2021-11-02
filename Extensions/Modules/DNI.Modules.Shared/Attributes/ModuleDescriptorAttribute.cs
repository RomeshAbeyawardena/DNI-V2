using System;

namespace DNI.Modules.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleDescriptorAttribute : Attribute
    {
        public ModuleDescriptorAttribute(
            string name,
            string uniqueIdentifier = default,
            string description = default,
            bool allowMultiple = false)
        {
            Name = name;

            if (Guid.TryParse(uniqueIdentifier, out var guid))
            {
                UniqueIdentifier = guid;
            }
            else UniqueIdentifier = Guid.NewGuid();

            Description = description;
            AllowMultiple = allowMultiple;
        }

        public Guid UniqueIdentifier { get; }
        public string Name { get; }
        public string Description { get; }
        public bool AllowMultiple { get; }
    }
}

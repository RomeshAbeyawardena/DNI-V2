using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Attributes
{
    /// <summary>
    /// Marks the field or property as a reciever for dependency injection resolution within an instance of an <see cref="Abstractions.IModule"/> either as a static or instance field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class ResolveAttribute : Attribute
    {
        public ResolveAttribute(Type resolverType = null)
        {
            ResolverType = resolverType;
        }

        public Type ResolverType { get; }
    }
}

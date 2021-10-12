using System.Reflection;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleAssemblyResolver
    {
        Assembly ResolveAssembly(string name);
    }
}

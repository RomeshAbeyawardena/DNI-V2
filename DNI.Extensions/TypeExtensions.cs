using DNI.Shared.Attributes; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Assembly> GetRequiredDependencyAssemblies(this Type type)
        {
            var assemblyList = new List<Assembly>();
            var requiresDependenciesAttribute = type.GetCustomAttribute<RequiresDependenciesAttribute>();

            if (requiresDependenciesAttribute != null)
            {
                assemblyList.AddRange(requiresDependenciesAttribute.RequiredTypes.Select(a => a.Assembly));
            }

            return assemblyList;
        }

        public static IEnumerable<Type> GetConstructorParameterTypes(this Type type)
        {
            var constructorWithParameters = type
                .GetConstructors()
                .FirstOrDefault(a => a.GetParameters().Length > 0);

            if (constructorWithParameters != null)
            {
                return constructorWithParameters.GetParameters().Select(p => p.ParameterType);
            }

            return Array.Empty<Type>();
        }
    }
}

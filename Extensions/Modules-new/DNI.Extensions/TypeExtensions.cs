using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class TypeExtensions
    {
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

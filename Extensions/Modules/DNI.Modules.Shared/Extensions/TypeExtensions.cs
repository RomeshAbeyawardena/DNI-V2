using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<object> ResolveStaticDependencies(this Type valueType, IServiceProvider moduleServiceProvider)
        {
            var resolvedServices = new List<object>();
            var properties = valueType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic);

            foreach (var propertyOrField in properties)
            {
                if (propertyOrField.GetCustomAttribute<ResolveAttribute>() == null)
                {
                    continue;
                }

                var service = moduleServiceProvider.GetRequiredService(propertyOrField.PropertyType);
                resolvedServices.Add(service);
                propertyOrField.SetValue(null, service);
            }

            return resolvedServices;
        }
    }
}

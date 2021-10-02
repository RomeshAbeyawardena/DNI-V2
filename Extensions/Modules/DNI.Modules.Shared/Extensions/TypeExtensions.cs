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
        public static void ResolveStaticDependencies(this Type valueType, IServiceProvider moduleServiceProvider)
        {
            var properties = valueType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                .AppendAll(valueType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic));

            foreach (var propertyOrField in properties)
            {
                if (propertyOrField.GetCustomAttribute<ResolveAttribute>() == null)
                {
                    continue;
                }

                var service = moduleServiceProvider.GetRequiredService(propertyOrField.PropertyType);
                propertyOrField.SetValue(null, service);
            }
        }
    }
}

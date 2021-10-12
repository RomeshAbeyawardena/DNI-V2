using DNI.Modules.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Modules.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<object> ResolveDependencies(this object value, IServiceProvider moduleServiceProvider)
        {
            var resolvedObjectsList = new List<object>();
            var valueType = value.GetType();
            var properties = valueType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var propertyOrField in properties)
            {
                if (propertyOrField.GetCustomAttribute<ResolveAttribute>() == null)
                {
                    continue;
                }

                var service = moduleServiceProvider.GetService(propertyOrField.PropertyType);
                resolvedObjectsList.Add(service);
                propertyOrField.SetValue(value, service);
            }

            return resolvedObjectsList;
        }
    }
}

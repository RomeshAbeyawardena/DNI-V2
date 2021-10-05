using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static void ResolveDependencies(this object value, IServiceProvider moduleServiceProvider)
        {
            var valueType = value.GetType();
            var properties = valueType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var propertyOrField in properties)
            {
                if (propertyOrField.GetCustomAttribute<ResolveAttribute>() == null)
                {
                    continue;
                }

                var service = moduleServiceProvider.GetService(propertyOrField.PropertyType);

                
                propertyOrField.SetValue(value, service);
            }
        }
    }
}

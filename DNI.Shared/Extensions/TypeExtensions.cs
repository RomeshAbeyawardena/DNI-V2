using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static IDictionary<PropertyInfo, TAttribute> GetPropertiesWithAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var properties = type.GetProperties();
            Dictionary<PropertyInfo, TAttribute> propertyAttributeDictionary = new();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<TAttribute>();

                if (attribute != null)
                {
                    propertyAttributeDictionary.Add(property, attribute);
                }
            }

            return propertyAttributeDictionary;
        }
    }
}

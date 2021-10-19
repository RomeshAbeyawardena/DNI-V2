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
        public static IDictionary<PropertyInfo, TAttribute> Invoke<TAttribute>(Type type, Action<bool, KeyValuePair<PropertyInfo, TAttribute>, IDictionary<PropertyInfo, TAttribute>> delegateAction)
            where TAttribute : Attribute
        {
            var properties = type.GetProperties();
            Dictionary<PropertyInfo, TAttribute> propertyAttributeDictionary = new();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<TAttribute>();

                delegateAction((attribute != null), KeyValuePair.Create(property, attribute), propertyAttributeDictionary);
            }

            return propertyAttributeDictionary;
        }

        public static IDictionary<PropertyInfo, TAttribute> GetPropertiesWithoutAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            static void conditionalAction(bool hasAttribute, KeyValuePair<PropertyInfo, TAttribute> item, IDictionary<PropertyInfo, TAttribute> dictionary)
            {
                if (hasAttribute)
                    dictionary.Add(item);
            }

            return Invoke<TAttribute>(type, conditionalAction);
        }

        public static IDictionary<PropertyInfo, TAttribute> GetPropertiesWithAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            static void conditionalAction(bool hasAttribute, KeyValuePair<PropertyInfo, TAttribute> item, IDictionary<PropertyInfo, TAttribute> dictionary)
            {
                if (!hasAttribute)
                    dictionary.Add(item);
            }

            return Invoke<TAttribute>(type, conditionalAction);
        }
    }
}

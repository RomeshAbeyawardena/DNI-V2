using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static void Extend<T>(this T model, T newModel)
        {
            var modelType = typeof(T);

            var enumerableType = typeof(IEnumerable<>);
            var listType = typeof(List<>);

            foreach (var property in modelType.GetProperties())
            {
                var propertyType = property.PropertyType;

                var isListType = false;
                var isEnumerableType = false;
                var genericArguments = propertyType.GetGenericArguments();
                var addRangeMethod = property.PropertyType.GetMethod("AddRange");
                if (propertyType.IsArray || propertyType.IsGenericType
                    && (isEnumerableType = propertyType == enumerableType.MakeGenericType(genericArguments))
                    || (isListType = propertyType == listType.MakeGenericType(genericArguments)))
                {
                    var newValues = property.GetValue(newModel);

                    if (isListType)
                    {
                        addRangeMethod?.Invoke(model, new[] { newValues });
                    }
                    else if (isEnumerableType)
                    {
                        var modelValue = property.GetValue(model);

                        var list = Activator.CreateInstance(listType.MakeGenericType(genericArguments), modelValue);
                        addRangeMethod.Invoke(list, new[] { newValues });
                        property.SetValue(model, list);
                    }
                }
            }
        }
    }
}

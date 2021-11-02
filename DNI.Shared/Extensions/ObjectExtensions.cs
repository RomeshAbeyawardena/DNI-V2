using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, object> GetDictionary<TAttribute>(this object item)
            where TAttribute : Attribute
        {
            return item.GetType()
                .GetPropertiesWithAttribute<TAttribute>()
                .ToDictionary(k => k.Key.Name, v => v.Key.GetValue(item));
        }

        public static bool IsDefault(this object value)
        {
            if (value is short _short && _short == default)
            {
                return true;
            }

            if (value is int _int && _int == default)
            {
                return true;
            }

            if (value is long _long && _long == default)
            {
                return true;
            }

            if (value is decimal _decimal && _decimal == default)
            {
                return true;
            }

            if (value is float _float && _float == default)
            {
                return true;
            }

            if (value is double _double && _double == default)
            {
                return true;
            }

            if (value is bool _bool && _bool == default)
            {
                return true;
            }

            if (value is byte _byte && _byte == default)
            {
                return true;
            }

            if (value is string _string && _string == default)
            {
                return true;
            }

            if (value is Guid _guid && _guid == default)
            {
                return true;
            }

            if (value is DateTimeOffset _dateTimeOffset && _dateTimeOffset == default)
            {
                return true;
            }

            if (value is DateTime _dateTime && _dateTime == default)
            {
                return true;
            }

            return false;
        }
    }
}

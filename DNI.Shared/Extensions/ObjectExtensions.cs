using System;

namespace DNI.Shared.Extensions
{
    public static class ObjectExtensions
    {
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

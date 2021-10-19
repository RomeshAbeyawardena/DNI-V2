﻿using DNI.Shared.Attributes;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DNI.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static object PerformMetaAction(MetaType metaType, Type value, 
            Func<bool, DateTimeOffset> offSetfactoryMethod = null,
            Func<bool, DateTime> factoryMethod = null)
        {
            object PerformDifferentMetaAction(MetaType metaType)
            {
                return PerformMetaAction(metaType, value, offSetfactoryMethod, factoryMethod);
            }

            switch (metaType)
            {
                case MetaType.Unspecified:
                    if (value == typeof(DateTimeOffset) || value == typeof(DateTimeOffset?))
                        return PerformDifferentMetaAction(MetaType.UtcDateTimeOffset);
                    else if (value == typeof(DateTime) || value == typeof(DateTime?))
                        return PerformDifferentMetaAction(MetaType.UtcDateTime);
                    else if (value == typeof(string))
                        return PerformDifferentMetaAction(MetaType.UtcDateTimeOffset).ToString();
                    else throw new NotSupportedException("Meta action can only be formed on date time instances");
                case MetaType.DateTime:
                    return factoryMethod(false);
                default:
                case MetaType.DateTimeOffset:
                    return offSetfactoryMethod(false);
                case MetaType.UtcDateTime:
                    return factoryMethod(true);
                case MetaType.UtcDateTimeOffset:
                    return offSetfactoryMethod(true);
            }
        }

        public static void UpdateMetaTags<T>(this T value, MetaAction metaAction,
            Func<bool, DateTimeOffset> offSetfactoryMethod = null,
            Func<bool, DateTime> factoryMethod = null)
        {
            if (offSetfactoryMethod == null)
            {
                offSetfactoryMethod = (useUtc) => useUtc ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
            }

            if (factoryMethod == null)
            {
                factoryMethod = (useUtc) => useUtc ? DateTime.UtcNow : DateTime.Now;
            }

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var metaAttribute = property.GetCustomAttribute<MetaPropertyAttribute>();

                if (metaAttribute == null || metaAttribute.MetaAction != metaAction)
                {
                    continue;
                }

                property.SetValue(value, PerformMetaAction(metaAttribute.MetaType, property.PropertyType,
                    offSetfactoryMethod, factoryMethod));
            }
        }

        public static bool ValidateETag<T>(this T value, string eTag)
        {
            return value.CalculateETag().Equals(eTag);
        }

        public static void UpdateETag<T>(this T value)
        {
            var properties = typeof(T).GetProperties();

            var eTag = value.CalculateETag(properties);

            foreach (var property in properties)
            {
                var eTagAttribute = property.GetCustomAttribute<ETagAttribute>();

                if (eTagAttribute == null)
                {
                    continue;
                }

                property.SetValue(value, eTag);
            }
        }

        public static string CalculateETag<T>(this T model, IEnumerable<PropertyInfo> properties = null)
        {
            StringBuilder stringBuilder = new();
            var modelType = typeof(T);

            if(properties == null)
            {
                properties = modelType.GetProperties();
            }

            foreach(var property in properties)
            {
                var eTagAttribute = property.GetCustomAttribute<ETagAttribute>();

                if (eTagAttribute != null)
                {
                    continue;
                }

                var modelValue = property.GetValue(model);
                if (!modelValue.IsDefault())
                {
                    stringBuilder.AppendFormat(":{0}", modelValue);
                }
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
        }

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

        public static void Set<T, TProperty, TValue>(this T value, Expression<Func<T, TProperty>> getProperty, TValue newValue)
        {
            PropertyInfo propertyInfo;
            Expression body = getProperty;
            if (body is LambdaExpression expression)
            {
                body = expression.Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    propertyInfo = (PropertyInfo)((MemberExpression)body).Member;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            propertyInfo.SetValue(value, newValue);
        }
    }
}

using DNI.MigrationManager.Shared.Abstractions;
using DNI.MigrationManager.Shared.Attributes;
using DNI.MigrationManager.Shared.Extensions;
using DNI.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DNI.MigrationManager.Core.Defaults
{
    public class DefaultDataColumn : IDataColumn
    {
        private readonly List<IForeignKey> foreignKeys;

        public DefaultDataColumn(ITableConfiguration tableConfiguration, PropertyInfo property)
        {
            foreignKeys = new List<IForeignKey>();
            Name = property.Name;
            Type = property.PropertyType;
            Configure(tableConfiguration, property);
        }

        private void Configure(ITableConfiguration tableConfiguration, PropertyInfo property)
        {
            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

            if (columnAttribute != null)
            {
                Name = columnAttribute.Name ?? Name;
                TypeName = columnAttribute.TypeName;
            }

            var maximumAttribute = property.GetCustomAttribute<MaxLengthAttribute>();

            if (maximumAttribute != null)
            {
                Length = maximumAttribute.Length;
            }

            var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

            if (defaultValueAttribute != null)
            {
                DefaultValue = defaultValueAttribute.Value;
            }

            var keyAttribute = property.GetCustomAttribute<KeyAttribute>();
            if (keyAttribute != null)
            {
                tableConfiguration.Set(a => a.PrimaryKey, Name);
            }

            var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
            var allowNullsAttribute = property.GetCustomAttribute<AllowNullsAttribute>();

            IsRequired = (requiredAttribute != null
                || allowNullsAttribute == null
                || (allowNullsAttribute != null && !allowNullsAttribute.Enabled));

            var referencesAttribute = property.GetCustomAttribute<ReferencesAttribute>();

            if (referencesAttribute != null)
            {
                if (!referencesAttribute.IsDbInfoResolved)
                {
                    referencesAttribute = referencesAttribute.ResolveDbInfo();

                    foreignKeys.Add(new DefaultForeignKey(tableConfiguration, referencesAttribute.TableConfiguration, referencesAttribute.ColumnName));
                }
            }
        }

        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public Type Type { get; }
        public object DefaultValue { get; private set; }
        public bool IsRequired { get; private set; }
        public int? Length { get; private set; }

        public IEnumerable<IForeignKey> ForeignKeys => foreignKeys;
    }
}

using DNI.MigrationManager.Shared.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DNI.MigrationManager.Shared.Extensions
{
    public static class ReferencesAttributeExtensions
    {
        public static ReferencesAttribute ResolveDbInfo(this ReferencesAttribute referencesAttribute, string defaultSchema = "dbo")
        {
            var property = referencesAttribute.ParentType.GetProperty(referencesAttribute.FieldOrPropertyName);
            TableAttribute tableAttribute;
            if ((tableAttribute = referencesAttribute.ParentType.GetCustomAttribute<TableAttribute>()) == null)
            {
                return new ReferencesAttribute(referencesAttribute.ParentType.Name, property.Name, defaultSchema);
            }

            return new ReferencesAttribute(tableAttribute.Name, property.Name, tableAttribute.Schema);
        }
    }
}

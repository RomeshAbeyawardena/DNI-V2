using DNI.Shared.Enumerations;
using System;

namespace DNI.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MetaPropertyAttribute : Attribute
    {
        public MetaType MetaType { get; }

        public MetaPropertyAttribute(MetaAction metaAction, MetaType metaType = MetaType.Unspecified)
        {
            MetaType = metaType;
            MetaAction = metaAction;
        }

        public MetaAction MetaAction { get; }
    }
}

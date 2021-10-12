using System;

namespace DNI.MigrationManager.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class AllowNullsAttribute : Attribute
    {
        public AllowNullsAttribute(bool enabled = true)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Determines whether this rule should apply
        /// </summary>
        public bool Enabled { get; }
    }
}

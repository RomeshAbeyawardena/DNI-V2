using DNI.Encryption.Shared.Enumerations;
using System;

namespace DNI.Encryption.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class EncryptionProfileAttribute : Attribute
    {
        public EncryptionProfileAttribute(string encryptionOptionsSectionName, EncryptionType encryptionType = EncryptionType.Encryption)
        {
            SectionName = encryptionOptionsSectionName;
            EncryptionType = encryptionType;
        }

        public string SectionName { get; }
        public EncryptionType EncryptionType { get; }
    }
}

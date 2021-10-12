using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Attributes
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

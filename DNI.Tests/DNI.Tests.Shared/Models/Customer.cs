using System;
using System.ComponentModel.DataAnnotations;
using DNI.Encryption.Shared.Attributes;
using DNI.Encryption.Shared.Enumerations;
using DNI.Shared.Attributes;

namespace DNI.Tests.Shared.Models
{
    public static class EncryptionSections
    {
        public const string Personal = "Personal";
        public const string Common = "Common";
        public const string Credential = "Credential";
        public const string Secondary = "Secondary";
    }

    public class Customer
    {
        [Key] public Guid Id { get; set; }

        [EncryptionProfile(EncryptionSections.Common, EncryptionType.Encryption)]
        public string FirstName { get; set; }

        [EncryptionProfile(EncryptionSections.Common, EncryptionType.Encryption)]
        public string MiddleName { get; set; }

        [EncryptionProfile(EncryptionSections.Common, EncryptionType.Encryption)]
        public string LastName { get; set; }

        [EncryptionProfile(EncryptionSections.Common, EncryptionType.Encryption)]
        public string Title { get; set; }

        [EncryptionProfile(EncryptionSections.Personal, EncryptionType.Encryption)]
        public string NationalInsuranceNumber { get; set; }

        [EncryptionProfile(EncryptionSections.Personal, EncryptionType.Encryption)]
        public string EmailAddress { get; set; }

        [EncryptionProfile(EncryptionSections.Personal, EncryptionType.Encryption)]
        public string BusinessEmailAddress { get; set; }

        [EncryptionProfile(EncryptionSections.Personal, EncryptionType.Encryption)]
        public string TelephoneNumber { get; set; }

        [EncryptionProfile(EncryptionSections.Personal, EncryptionType.Encryption)]
        public string MobileNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MetaProperty(DNI.Shared.Enumerations.MetaAction.Add)]
        public DateTimeOffset Created { get; set; }

        [MetaProperty(DNI.Shared.Enumerations.MetaAction.Update)]
        public DateTimeOffset? Modified { get; set; }
    }
}

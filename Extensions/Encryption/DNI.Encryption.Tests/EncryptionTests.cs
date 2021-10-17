using DNI.Encryption.Core.Defaults;
using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Base;
using DNI.Encryption.Shared.Enumerations;
using Moq;
using NUnit.Framework;
using System.Text;

namespace DNI.Encryption.tests
{
    public class EncryptionTests
    {
        private DefaultDecryptor sutDecryptor;
        private DefaultEncryptor sutEncryptor;
        private Mock<IEncryptionOptions> encryptionOptionsMock;
        private Mock<ISymmetricAlgorithmFactory> symmetricAlgorithmFactoryMock;

        [SetUp]
        public void Setup()
        {
            encryptionOptionsMock = new Mock<IEncryptionOptions>();
            symmetricAlgorithmFactoryMock = new Mock<ISymmetricAlgorithmFactory>();
            sutEncryptor = new DefaultEncryptor(encryptionOptionsMock.Object, symmetricAlgorithmFactoryMock.Object);
            sutDecryptor = new DefaultDecryptor(encryptionOptionsMock.Object, symmetricAlgorithmFactoryMock.Object);
        }

        [Test]
        public void Encrypted_value_should_be_the_same_value_when_decrypted_using_the_same_keys_used_during_encryption()
        {
            var expected = "test";
            encryptionOptionsMock.Setup(a => a.Algorithm)
                .Returns(Shared.Enumerations.SymmetricAlgorithm.Aes);
            encryptionOptionsMock.Setup(a => a.Encoding)
                .Returns(Encoding.UTF8);
            encryptionOptionsMock.Setup(a => a.InitialVector)
                .Returns("YmQ1MzkyMWRjOWQ4YTU3ZA==");
            encryptionOptionsMock.Setup(a => a.Key)
                .Returns("NWU1Nzc0ZTZkYjJlNDI3ZmI5MzVkZmZiYWJkODJlZjA=");

            symmetricAlgorithmFactoryMock.Setup(a => a.GetSymmetricAlgorithm(SymmetricAlgorithm.Aes))
                .Returns(System.Security.Cryptography.SymmetricAlgorithm.Create("AES"));

            var encryptedValue = sutEncryptor.Encrypt(expected, encryptionOptionsMock.Object);
            var decryptedValue = sutDecryptor.Decrypt(encryptedValue, encryptionOptionsMock.Object);
            Assert.AreNotEqual(expected, encryptedValue);
            Assert.AreEqual(expected, decryptedValue);
        }
    }
}
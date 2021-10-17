using DNI.Encryption.Modules;
using DNI.Encryption.Shared.Abstractions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DNI.Encryption.Tests
{
    public class EncryptionModuleTests
    {
        private const string ConfigureEncryptionOptions = "Configure_Encryption_Options";
        private EncryptionModule sut;
        private Mock<IServiceProvider> serviceCollectionMock;
        private Mock<IConfiguration> configurationMock;
        private Mock<IConfigurationSection> configurationSecurityProfileMock;
        private Mock<IConfigurationSection> configurationGeneralMock;
        private Mock<IConfigurationSection> configurationGeneralSectionMock;
        private Mock<IEncryptionModuleOptions> encryptionModuleOptionsMock;
        private Dictionary<string, IEncryptionOptions> optionsDictionary;
        private string testFunction = string.Empty;




        [SetUp]
        public void Setup()
        {
            sut = new EncryptionModule();
            optionsDictionary = new Dictionary<string, IEncryptionOptions>();
            serviceCollectionMock = new Mock<IServiceProvider>();
            configurationMock = new Mock<IConfiguration>();
            configurationSecurityProfileMock = new Mock<IConfigurationSection>();
            configurationGeneralMock = new Mock<IConfigurationSection>();
            encryptionModuleOptionsMock = new Mock<IEncryptionModuleOptions>();
            configurationGeneralSectionMock = new Mock<IConfigurationSection>();


            serviceCollectionMock.Setup(a => a.GetService(typeof(IConfiguration)))
           .Returns(configurationMock.Object);

            configurationMock.Setup(a => a.GetSection("SecurityProfiles"))
                .Returns(configurationSecurityProfileMock.Object);

            configurationSecurityProfileMock.Setup(a => a.GetSection("General"))
                .Returns(configurationGeneralMock.Object);

            configurationGeneralMock.Setup(a => a.GetChildren())
                .Returns(new[] { configurationGeneralSectionMock.Object });

            encryptionModuleOptionsMock.Setup(a => a.EncryptionOptions)
                .Returns(optionsDictionary);

            encryptionModuleOptionsMock.Setup(a => a.ImportConfiguration)
                .Returns(true);

            encryptionModuleOptionsMock.Setup(a => a.ImportConfigurationPath)
                .Returns("SecurityProfiles/General");

        }

        [Test]
        public void Configure_Encryption_Options()
        {
            sut.ConfigureEncryptionOptions(serviceCollectionMock.Object, encryptionModuleOptionsMock.Object);
        }
    }
}

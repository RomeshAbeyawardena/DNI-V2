using DNI.Encryption.Modules;
using DNI.Encryption.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Tests
{
    public class EncryptionModuleTests
    {
        private EncryptionModule sut;
        private Mock<IServiceCollection> serviceCollectionMock = new Mock<IServiceCollection>();
        private Mock<IModuleConfiguration> moduleConfigurationMock = new Mock<IModuleConfiguration>();
        private Mock<IEncryptionModuleOptions> encryptionModuleOptionsMock = new Mock<IEncryptionModuleOptions>();
        private Dictionary<Type, object> optionsDictionary;
        [SetUp]
        public void Setup()
        {
            sut = new EncryptionModule();
            optionsDictionary = new Dictionary<Type, object>();
            serviceCollectionMock = new Mock<IServiceCollection>();
            moduleConfigurationMock = new Mock<IModuleConfiguration>();
            encryptionModuleOptionsMock = new Mock<IEncryptionModuleOptions>();
        }

        [Test]
        public void Test()
        {
            optionsDictionary.Add(typeof(IEncryptionModuleOptions), encryptionModuleOptionsMock.Object);

            moduleConfigurationMock.Setup(a => a.Options)
                .Returns(optionsDictionary);
            sut.ConfigureServices(serviceCollectionMock.Object,
                moduleConfigurationMock.Object);
        }
    }
}

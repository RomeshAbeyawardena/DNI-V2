using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Services;
using DNI.Shared.Extensions;
using DNI.Shared.Services;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DNI.Shared.UnitTests
{
    public class PlaceholderServiceTests
    {
        private IPlaceholderService placeholderService;
        private Mock<IServiceConfig> serviceConfigMock;
        [SetUp]
        public void Setup()
        {
            serviceConfigMock = new Mock<IServiceConfig>();
            placeholderService = new PlaceholderService(serviceConfigMock.Object);
        }

        [Test]
        public void GetPlaceholders()
        {
            serviceConfigMock.Setup(a => a.ThrowOnHandledExceptions).Returns(false);
            var placeholders = placeholderService.ExtractPlaceholders("Hello [name] of [place]", '[', ']');
            Assert.IsNotEmpty(placeholders);
            Assert.Contains("name", placeholders.Select(a => a.Value).ToArray());
            Assert.Contains("place", placeholders.Select(a => a.Value).ToArray());
        }

        [TestCase("John", "Spain", true)]
        [TestCase("Sally", "Washington", true)]
        [TestCase("Jane", "Bankok", true)]
        [TestCase("Harry", null, false)]
        public void ReplacePlaceholders(string name, string place, bool throwOnHandledExceptions)
        {
            serviceConfigMock.Setup(a => a.ThrowOnHandledExceptions).Returns(throwOnHandledExceptions);
            var originalText = "Hello [name] of [place]";
            var placeholders = placeholderService.ExtractPlaceholders(originalText, '[', ']');
            var replacedText = placeholderService.ReplacePlaceholders(originalText, placeholders, dic => dic.Add("name", name).Add("place", place));

            Assert.AreEqual($"Hello {name} of {place}", replacedText);
        }    
    }
}
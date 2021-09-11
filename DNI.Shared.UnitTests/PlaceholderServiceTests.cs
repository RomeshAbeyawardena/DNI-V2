using DNI.Shared.Abstractions.Services;
using DNI.Shared.Extensions;
using DNI.Shared.Services;
using NUnit.Framework;
using System.Linq;

namespace DNI.Shared.UnitTests
{
    public class PlaceholderServiceTests
    {
        private IPlaceholderService placeholderService;

        [SetUp]
        public void Setup()
        {
            placeholderService = new PlaceholderService();
        }

        [Test]
        public void GetPlaceholders()
        {
            var placeholders = placeholderService.ExtractPlaceholders("Hello [name] of [place]", '[', ']');
            Assert.IsNotEmpty(placeholders);
            Assert.Contains("name", placeholders.Select(a => a.Value).ToArray());
            Assert.Contains("place", placeholders.Select(a => a.Value).ToArray());
        }

        [TestCase("John", "Spain")]
        [TestCase("Sally", "Washington")]
        [TestCase("Jane", "Bankok")]
        public void ReplacePlaceholders(string name, string place)
        {
            var originalText = "Hello [name] of [place]";
            var placeholders = placeholderService.ExtractPlaceholders(originalText, '[', ']');
            var replacedText = placeholderService.ReplacePlaceholders(originalText, placeholders, dic => dic.Add("name", name).Add("place", place));

            Assert.AreEqual($"Hello {name} of {place}", replacedText);
        }    
    }
}
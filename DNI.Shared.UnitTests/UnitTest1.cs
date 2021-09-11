using DNI.Shared.Extensions;
using NUnit.Framework;
using System.Linq;

namespace DNI.Shared.UnitTests
{
    public class StringExtensionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetPlaceholders()
        {
            var placeholders = StringExtensions.GetPlaceholders("Hello [name] of [place]", '[', ']');
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
            var placeholders = StringExtensions.GetPlaceholders(originalText, '[', ']');
            var replacedText = originalText.ReplacePlaceholders(placeholders, dic => dic.Add("name", name).Add("place", place));

            Assert.AreEqual($"Hello {name} of {place}", replacedText);
        }    
    }
}
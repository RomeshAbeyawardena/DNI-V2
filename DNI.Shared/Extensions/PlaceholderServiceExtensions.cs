using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Extensions
{
    public static class PlaceholderServiceExtensions
    {
        public static string ReplacePlaceholders(this IPlaceholderService placeholderService, string value, IEnumerable<PlaceholderInfo> placeholders, Action<IDictionaryBuilder<string, string>> build)
        {
            var dictionaryBuilder = DictionaryBuilder.Build(build);
            return placeholderService.ReplacePlaceholders(value, placeholders, dictionaryBuilder);
        }

        public static string ReplacePlaceholders(this IPlaceholderService placeholderService, string value, char startCharacter, char endCharacter, IDictionary<string, Func<string, string>> replacements)
        {
            var placeholders = placeholderService.ExtractPlaceholders(value, startCharacter, endCharacter);
            return placeholderService.ReplacePlaceholders(value, placeholders, replacements);
        }

        public static string ReplacePlaceholders(this IPlaceholderService placeholderService, string value, char startCharacter, char endCharacter, IDictionary<string, string> replacements)
        {
            var placeholders = placeholderService.ExtractPlaceholders(value, startCharacter, endCharacter);
            return placeholderService.ReplacePlaceholders(value, placeholders, replacements);
        }

    }
}

using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ReplacePlaceholders(this string value, IEnumerable<PlaceholderInfo> placeholders, Action<IDictionaryBuilder<string, string>> build)
        {
            var dictionaryBuilder = DictionaryBuilder.Build(build);
            return ReplacePlaceholders(value, placeholders, dictionaryBuilder);
        }

        public static string ReplacePlaceholders(this string value, IEnumerable<PlaceholderInfo> placeholders, IDictionary<string, string> replacements)
        {
            return ReplacePlaceholders(value, placeholders, replacements.ToDictionary(a => a.Key, a => GetReplacementFactory(a.Value)));
        }

        public static string ReplacePlaceholders(this string value, char startCharacter, char endCharacter, IDictionary<string, Func<string, string>> replacements)
        {
            var placeholders = GetPlaceholders(value, startCharacter, endCharacter);
            return ReplacePlaceholders(value, placeholders, replacements);
        }

        public static string ReplacePlaceholders(this string value, char startCharacter, char endCharacter, IDictionary<string, string> replacements)
        {
            var placeholders = GetPlaceholders(value, startCharacter, endCharacter);
            return ReplacePlaceholders(value, placeholders, replacements);
        }

        private static Func<string, string> GetReplacementFactory(string arg)
        {
            return a => arg;
        }

        public static string ReplacePlaceholders(this string value, IEnumerable<PlaceholderInfo> placeholders, IDictionary<string, Func<string, string>> replacements)
        {
            foreach (var placeholder in placeholders)
            {
                var str = $"{placeholder.StartCharacter}{placeholder.Value}{placeholder.EndCharacter}";
                value = value.Replace(str, replacements[placeholder.Value]?.Invoke(str));
            }

            return value;
        }

        public static IEnumerable<PlaceholderInfo> GetPlaceholders(this string value, char startCharacter, char endCharacter)
        {
            var placeholderInfoList = new List<PlaceholderInfo>();
            string currentPlaceholder = string.Empty;
            int currentCharacterIndex = 0;
            bool isPlaceholder = false;

            foreach (var character in value)
            {
                if (character == startCharacter)
                {
                    isPlaceholder = true;
                    continue;
                }

                if(character == endCharacter)
                {
                    isPlaceholder = false;
                    placeholderInfoList.Add(new PlaceholderInfo(startCharacter, endCharacter, currentPlaceholder, 
                        currentCharacterIndex - currentPlaceholder.Length));
                    currentPlaceholder = string.Empty;
                    continue;
                }

                if (isPlaceholder)
                {
                    currentPlaceholder += character;
                }

                currentCharacterIndex++;
            }

            return placeholderInfoList;
        }
    }
}

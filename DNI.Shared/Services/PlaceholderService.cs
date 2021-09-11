using DNI.Shared.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Services
{
    public class PlaceholderService : IPlaceholderService
    {
        public IEnumerable<PlaceholderInfo> ExtractPlaceholders(string value, char startCharacter, char endCharacter)
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

                if (character == endCharacter)
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

        public string ReplacePlaceholders(string value, IEnumerable<PlaceholderInfo> placeholders, IDictionary<string, string> replacements)
        {
            return ReplacePlaceholders(value, placeholders, replacements.ToDictionary(a => a.Key, a => GetReplacementFactory(a.Value)));
        }

        public string ReplacePlaceholders(string value, IEnumerable<PlaceholderInfo> placeholders, IDictionary<string, Func<string, string>> replacementsFactory)
        {
            foreach (var placeholder in placeholders)
            {
                var str = $"{placeholder.StartCharacter}{placeholder.Value}{placeholder.EndCharacter}";
                if(replacementsFactory.TryGetValue(placeholder.Value, out var factory))
                {
                    value = value.Replace(str, factory?.Invoke(str) ?? throw new NullReferenceException());
                }
            }

            return value;
        }


        private static Func<string, string> GetReplacementFactory(string arg)
        {
            return a => arg;
        }
    }
}

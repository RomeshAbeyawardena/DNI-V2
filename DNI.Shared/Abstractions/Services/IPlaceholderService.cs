using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Services
{
    public interface IPlaceholderService : IService
    {
        IEnumerable<PlaceholderInfo> ExtractPlaceholders(string value, char startCharacter, char endCharacter);
        string ReplacePlaceholders(string value, IEnumerable<PlaceholderInfo> placeholders, IDictionary<string, string> replacements);
        string ReplacePlaceholders(string value, IEnumerable<PlaceholderInfo> placeholders, IDictionary<string, Func<string, string>> replacementsFactory);
    }
}

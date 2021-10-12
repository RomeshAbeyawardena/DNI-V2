using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Extensions;
using System.IO;
using System.Text.Json;

namespace DNI.Modules.Shared.Extensions
{
    public static class IncludeExtensions
    {
        public static T Extend<T>(this IIncludeConfiguration configuration)
            where T : IIncludeConfiguration
        {
            if (!string.IsNullOrWhiteSpace(configuration.IncludePath))
            {
                using (var textStream = File.OpenText(configuration.IncludePath))
                {
                    var model = JsonSerializer.Deserialize<T>(textStream.ReadToEnd());

                    configuration.Extend(model);
                }
            }

            return (T)configuration;
        }
    }
}

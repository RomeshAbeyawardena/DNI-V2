using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

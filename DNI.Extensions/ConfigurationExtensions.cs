using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration ResolvePath(this IConfiguration configuration, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return configuration;
            }

            var paths = path.Split("/", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var currentSection = configuration;

            foreach(var _path in paths)
            {
                var foundSection = currentSection.GetSection(_path);

                if(foundSection != null)
                {
                    //section found, lets look at the next part of the path
                    currentSection = foundSection;
                }
                else
                {
                    //section not found, lets give the section that we could find last instead!
                    return currentSection;
                }
            }

            return currentSection;
        }
    }
}

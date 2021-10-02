using DNI.Shared.Abstractions.Hosts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class ConsoleHostExtensions
    {
        public static IConsoleHost Configure(this IConsoleHost consoleHost, Action<IConfigurationBuilder> configure)
        {
            consoleHost.Services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .Build());
            return consoleHost;
        }
    }
}

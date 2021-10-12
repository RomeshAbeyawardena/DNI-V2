﻿using DNI.Shared.Abstractions.Hosts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DNI.Extensions
{
    public static class ConsoleHostExtensions
    {
        public static IConsoleHost AddConfiguration(this IConsoleHost consoleHost, Action<IConfigurationBuilder> configure)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configure(configurationBuilder);
            consoleHost.Services.AddSingleton<IConfiguration>(configurationBuilder
                .Build());
            return consoleHost;
        }

        public static IConsoleHost AddLogging(this IConsoleHost consoleHost, Action<ILoggingBuilder> configure)
        {
            consoleHost.Services.AddLogging(configure);
            return consoleHost;
        }
    }
}

﻿using DNI.Core.Defaults.Hosts;
using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            await ConsoleHost.Build(h => h
                .ConfigureServices<Startup>(s => s.AddMigration("Default", Configure))).StartAsync();
        }

        private static IMigrationOptions Configure(IServiceProvider arg1, IMigrationConfigurator arg2)
        {
            throw new NotImplementedException();
        }
    }
}

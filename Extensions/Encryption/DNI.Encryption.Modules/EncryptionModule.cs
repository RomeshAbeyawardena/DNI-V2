using DNI.Core;
using DNI.Core.Defaults.Builders;
using DNI.Encryption.Core.Defaults;
using DNI.Encryption.Extensions;
using DNI.Encryption.Shared.Abstractions;
using DNI.Extensions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Hosts;
using DNI.Shared.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Encryption.Modules
{
    [RequiresDependencies(typeof(Core.This))]
    public class EncryptionModule : ModuleBase
    {
        internal IKeyValuePair<string, IEncryptionOptionsConfiguration> Configure(IConfigurationSection configuration)
        {
            return DefaultKeyValuePair
                .Create(configuration.Key,
                (IEncryptionOptionsConfiguration)new DefaultEncryptionOptionsConfiguration(configuration));
        }

        internal IDictionary<string, IEncryptionOptions> ConfigureEncryptionOptions(
            IServiceProvider serviceProvider,
            IEncryptionModuleOptions encryptionModuleOptions)
        {
            var db = DictionaryBuilder.Create(encryptionModuleOptions.EncryptionOptions);

            if (encryptionModuleOptions.ImportConfiguration)
            {
                var configuration = serviceProvider.GetService<IConfiguration>() ?? throw new NullReferenceException($"Unable to find service {typeof(IConfiguration)}, " +
                    $"if you are using an instance of {typeof(IConsoleHost)}, ensure you call an overload of AddDefaultConfiguration extension method to register necessary configuration");

                var configurationSection = configuration.ResolvePath(encryptionModuleOptions.ImportConfigurationPath);

                var configChildren = configurationSection.GetChildren();
                configChildren.Select(Configure).ForEach(a => db.Add(a.Key == "default"
                    ? string.Empty
                    : a.Key, a.Value.Build()));
            }

            encryptionModuleOptions.EncryptionOptionsFactory.ForEach(a => db.Add(a.Key, a.Value(serviceProvider)));

            return db.Dictionary;
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            services.AddEncryptionServices();
            var encryptionModuleOptions = moduleConfiguration.GetOptions<IEncryptionModuleOptions>();

            services.AddSingleton(encryptionModuleOptions);

            services.AddSingleton(s => encryptionModuleOptions.EncryptionOptions
                .TryGetValue(string.Empty, out var encryptionOptions)
                ? encryptionOptions : default);

            services.AddSingleton(s => ConfigureEncryptionOptions(s, encryptionModuleOptions));
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

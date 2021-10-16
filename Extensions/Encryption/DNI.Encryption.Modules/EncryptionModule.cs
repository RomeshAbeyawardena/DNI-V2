using DNI.Core.Defaults.Builders;
using DNI.Encryption.Extensions;
using DNI.Encryption.Shared.Abstractions;
using DNI.Extensions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Encryption.Modules
{
    [RequiresDependencies(typeof(Core.This))]
    public class EncryptionModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            services.AddEncryptionServices();
            var encryptionModuleOptions = moduleConfiguration.GetOptions<IEncryptionModuleOptions>();

            services.AddSingleton(s => { 
                var db = DictionaryBuilder.Create(encryptionModuleOptions.EncryptionOptions);

                encryptionModuleOptions.EncryptionOptionsFactory.ForEach(a => db.Add(a.Key, a.Value(s)));

                return db.Dictionary;
            });
        }

        public override void OnDispose(bool disposing)
        {
            throw new NotImplementedException();
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

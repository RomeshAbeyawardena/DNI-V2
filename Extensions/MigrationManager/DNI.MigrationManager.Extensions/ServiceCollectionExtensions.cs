using DNI.Core;
using DNI.Core.Defaults.Builders;
using DNI.MigrationManager.Core.Defaults;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.MigrationManager.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrationServices(this IServiceCollection services)
        {
            return services
                .AddDbTypeDefinitions("Sql", dictionaryBuilder => dictionaryBuilder
                    .Add(typeof(Guid), "UNIQUEIDENTIFIER")
                    .Add(typeof(short), "SMALLINT")
                    .Add(typeof(DateTimeOffset), "DATETIMEOFFSET")
                    .Add(typeof(DateTime), "DATETIME")
                    .Add(typeof(bool), "BIT")
                    .Add(typeof(byte), "TINYINT")
                    .Add(typeof(int), "INT")
                    .Add(typeof(string), "VARCHAR(#length)")
                    .Add(typeof(float), "FLOAT")
                    .Add(typeof(long), "BIGINT")
                    .Add(typeof(decimal), "DECIMAL(#length)"));
        }

        public static IServiceCollection AddMigration(this IServiceCollection service, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {
            return service.AddSingleton(s => s.ConfigureMigration(migrationName, build));
        }

        public static IKeyValuePair<string, IMigrationOptions> ConfigureMigration(this IServiceProvider serviceProvider, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {
            var migrationConfigurator = serviceProvider.GetRequiredService<IMigrationConfigurator>();
            var migrationOptions = build(serviceProvider, migrationConfigurator);
            return DefaultKeyValuePair.Create(migrationName, migrationOptions);
        }

        public static IServiceCollection AddDbTypeDefinitions(this IServiceCollection services, string dbType, Action<IDictionaryBuilder<Type, string>> build)
        {
            var dictionaryBuilder = new DefaultDictionaryBuilder<Type, string>();

            build?.Invoke(dictionaryBuilder);
            return services.AddSingleton(DefaultKeyValuePair.Create(dbType, DefaultDbTypeDefinitions.Create(dictionaryBuilder.Dictionary)));
        }
    }
}

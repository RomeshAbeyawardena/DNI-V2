using DNI.MigrationManager.Shared.Abstractions.Builders;
using System;

namespace DNI.MigrationManager.Shared.Abstractions
{
    /// <summary>
    /// Represents a class that can configure a migration on specific <see cref="Type">types</see> or all <see cref="Type">types</see> within a list of <see cref="System.Reflection.Assembly">assemblies</see>
    /// </summary>
    public interface IMigrationConfigurator
    {
        /// <summary>
        /// Configures the migration options using a default <see cref="IMigrationConfiguratorOptionsBuilder"/>
        /// </summary>
        /// <param name="configure">Configures the <see cref="IMigrationConfigurator"></see></param>
        /// <returns></returns>
        IMigrationConfigurator Configure(Action<IMigrationConfiguratorOptionsBuilder> configure);
        IMigrationOptions Build();
    }
}

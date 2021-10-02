﻿using DNI.MigrationManager.Shared.Abstractions.Builders;
using DNI.MigrationManager.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultDatabaseQueryBuilderFactory : IDatabaseQueryBuilderFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultDatabaseQueryBuilderFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IDatabaseQueryBuilder GetDatabaseQueryBuilder(string name)
        {
            var databaseQueryBuilders = serviceProvider.GetServices<IDatabaseQueryBuilder>();
            return databaseQueryBuilders.FirstOrDefault(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

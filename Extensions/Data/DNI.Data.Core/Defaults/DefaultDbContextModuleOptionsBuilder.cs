using DNI.Data.Shared.Abstractions;
using DNI.Data.Shared.Abstractions.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DNI.Data.Core.Defaults
{
    public class DefaultDbContextModuleOptionsBuilder : IDbContextModuleOptionsBuilder
    {
        private readonly List<IDbContextTypeOptions> options;

        public DefaultDbContextModuleOptionsBuilder()
        {
            options = new List<IDbContextTypeOptions>();
        }

        public IDbContextModuleOptionsBuilder AddDbContext<TDbContext>(Action<DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext
        {
            options.Add(new DefaultDbContextTypeOptions(typeof(TDbContext), buildAction: buildAction, serviceLifetime: serviceLifetime));
            return this;
        }

        public IDbContextModuleOptionsBuilder AddDbContext<TDbContext>(Action<IServiceProvider, DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext
        {
            options.Add(new DefaultDbContextTypeOptions(typeof(TDbContext), factorybuildAction: buildAction, serviceLifetime: serviceLifetime));
            return this;
        }

        public IDbContextModuleOptions Build()
        {
            return new DefaultDbContextModuleOptions(options);
        }
    }
}

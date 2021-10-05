using DNI.Data.Shared.Abstractions;
using DNI.Data.Shared.Abstractions.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Core.Defaults
{
    public class DefaultDbContextModuleOptionsBuilder : IDbContextModuleOptionsBuilder
    {
        private readonly List<IDbContextTypeOptions> options;

        public DefaultDbContextModuleOptionsBuilder(IServiceCollection services)
        {
            options = new List<IDbContextTypeOptions>();
        }

        public IDbContextModuleOptionsBuilder AddDbContext<DbContext>(Action<DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
        {
            options.Add(new DefaultDbContextTypeOptions());
            return this;
        }

        public IDbContextModuleOptionsBuilder AddDbContext<DbContext>(Action<IServiceProvider, DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
        {
            options.Add(new DefaultDbContextTypeOptions());
            return this;
        }

        public IDbContextModuleOptions Build()
        {
            return new DefaultDbContextModuleOptions(options);
        }
    }
}

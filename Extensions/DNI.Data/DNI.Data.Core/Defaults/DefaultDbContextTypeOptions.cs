using DNI.Data.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Core.Defaults
{
    public class DefaultDbContextTypeOptions : IDbContextTypeOptions
    {
        public DefaultDbContextTypeOptions(
            Type dbContextType,
            Action<IServiceProvider, DbContextOptionsBuilder> factorybuildAction = null,
            Action<DbContextOptionsBuilder> buildAction = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            Type = dbContextType;
            DbContextOptionsFactoryBuilder = factorybuildAction;
            DbContextOptionsBuilder = buildAction;
            ServiceLifetime = serviceLifetime;
        }

        public Action<IServiceProvider, DbContextOptionsBuilder> DbContextOptionsFactoryBuilder { get; }
        public Action<DbContextOptionsBuilder> DbContextOptionsBuilder { get; }

        public ServiceLifetime ServiceLifetime { get; }

        public Type Type { get; }
    }
}

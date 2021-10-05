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
        public DefaultDbContextTypeOptions(Action<IServiceProvider, DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
        {
            DbContextOptionsBuilder = buildAction;
            ServiceLifetime = serviceLifetime;
        }

        public Action<IServiceProvider, DbContextOptionsBuilder> DbContextOptionsBuilder { get; }

        public ServiceLifetime ServiceLifetime { get; }

        public Type Type => throw new NotImplementedException();
    }
}

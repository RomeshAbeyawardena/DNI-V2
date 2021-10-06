using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Shared.Abstractions.Builders
{
    public interface IDbContextModuleOptionsBuilder
    {
        IDbContextModuleOptionsBuilder AddDbContext<TDbContext>(Action<DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext;
        IDbContextModuleOptionsBuilder AddDbContext<TDbContext>(Action<IServiceProvider, DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext;

        IDbContextModuleOptions Build();
    }
}

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
        IDbContextModuleOptionsBuilder AddDbContext<DbContext>(Action<DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime);
        IDbContextModuleOptionsBuilder AddDbContext<DbContext>(Action<IServiceProvider, DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime);

        IDbContextModuleOptions Build();
    }
}

using DNI.Modules.Shared.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Data.Shared.Abstractions.Builders
{
    public interface IDbContextModuleOptionsBuilder : IModuleOptionsBuilder<IDbContextModuleOptions>
    {
        IDbContextModuleOptionsBuilder AddDbContext<TDbContext>(Action<DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext;
        IDbContextModuleOptionsBuilder AddDbContext<TDbContext>(Action<IServiceProvider, DbContextOptionsBuilder> buildAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext;
    }
}

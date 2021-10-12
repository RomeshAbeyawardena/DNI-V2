using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Data.Shared.Abstractions
{
    public interface IDbContextTypeOptions
    {
        Action<DbContextOptionsBuilder> DbContextOptionsBuilder { get; }
        Action<IServiceProvider, DbContextOptionsBuilder> DbContextOptionsFactoryBuilder { get; }
        ServiceLifetime ServiceLifetime { get; }
        Type Type { get; }
    }
}

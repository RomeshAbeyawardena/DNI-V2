using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

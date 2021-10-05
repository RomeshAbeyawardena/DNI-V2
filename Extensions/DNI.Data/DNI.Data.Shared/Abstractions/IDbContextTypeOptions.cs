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
        Action<IServiceProvider, DbContextOptionsBuilder> DbContextOptionsBuilder { get; }
        ServiceLifetime ServiceLifetime { get; }
        Type Type { get; }
    }
}

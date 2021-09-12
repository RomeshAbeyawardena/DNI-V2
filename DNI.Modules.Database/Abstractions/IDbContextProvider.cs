using DNI.Shared.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database.Abstractions
{
    public interface IDbContextProvider<TDbContext>
       where TDbContext : DbContext
    {
        IDbContextProvider<TDbContext> RegisterEntitiesWithAttribute<TAttribute>(params Assembly[] assemblies)
            where TAttribute : EntityAttribute;
        IDbContextProvider<TDbContext> RegisterEntitiesWithEntityAttribute(params Assembly[] assemblies);
        IDbContextProvider<TDbContext> RegisterEntity<TEntity>()
            where TEntity : class;
        IDbContextProvider<TDbContext> Using(params Assembly[] assemblies);
        IEnumerable<IRegisteredEntity<TDbContext>> Build(IServiceCollection services);
    }
}

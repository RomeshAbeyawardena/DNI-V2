using DNI.Modules.Database.Abstractions;
using DNI.Shared.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database
{
    public class DbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        private readonly List<IRegisteredEntity<TDbContext>> entityTypes;

        public DbContextProvider()
        {
            entityTypes = new List<IRegisteredEntity<TDbContext>>();
        }

        public IEnumerable<IRegisteredEntity<TDbContext>> Build(IServiceCollection services)
        {
            services.AddSingleton(entityTypes);

            return entityTypes;
        }

        public IDbContextProvider<TDbContext> RegisterEntitiesWithEntityAttribute(params Assembly[] assemblies)
        {
            return RegisterEntitiesWithAttribute<EntityAttribute>(assemblies);
        }

        public IDbContextProvider<TDbContext> RegisterEntitiesWithAttribute<TAttribute>(params Assembly[] assemblies) where TAttribute : EntityAttribute
        {
            if (!assemblies.Any())
            {
                assemblies = Assemblies?.ToArray() ?? new[] { Assembly.GetCallingAssembly() };
            }

            foreach (var assembly in assemblies)
            {
                var entityTypes = assembly.GetTypes().Where(IsAttributeNotNullAndEnabled<TAttribute>);
                foreach (var type in entityTypes)
                {
                    var dbContextProviderType = typeof(DbContextProvider<TDbContext>);
                    var registerEntityMethod = dbContextProviderType.GetMethod("RegisterEntity");
                    registerEntityMethod.MakeGenericMethod(type).Invoke(this, Array.Empty<object>());
                }
            }

            return this;
        }

        public IDbContextProvider<TDbContext> RegisterEntity<TEntity>() where TEntity : class
        {
            entityTypes.Add(RegisteredEntity<TDbContext>.Register<TEntity>());
            return this;
        }

        private static bool IsAttributeNotNullAndEnabled<TAttribute>(Type type)
            where TAttribute : EntityAttribute
        {
            var attribute = type.GetCustomAttribute<TAttribute>();

            return attribute != null && attribute.Enabled;
        }

        public IDbContextProvider<TDbContext> Using(params Assembly[] assemblies)
        {
            Assemblies = assemblies;
            return this;
        }

        private IEnumerable<Assembly> Assemblies { get; set; }
    }
}

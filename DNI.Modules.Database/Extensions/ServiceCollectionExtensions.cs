using DNI.Modules.Database.Abstractions;
using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static bool IsDbSet(this Type type)
        {
            try
            {
                if (!type.IsGenericType)
                {
                    return false;
                }

                var genericArguments = type.GetGenericArguments();

                var dbSetType = typeof(DbSet<>);

                return type == dbSetType.MakeGenericType(genericArguments);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static IServiceCollection RegisterRepositories<TDbContext>(this IServiceCollection services,
           IEnumerable<Assembly> assemblies)
           where TDbContext : DbContext
        {
            var genericServiceType = typeof(IRepository<>);
            var genericServiceImplementation = typeof(EntityFrameworkRepository<,>);
            var dbContextType = typeof(TDbContext);
            var dbSetProperties = typeof(TDbContext).GetProperties().Where(property => property.PropertyType.IsDbSet());

            var registeredEntities = services.RegisterEntities<TDbContext>(assemblies);
            foreach (var registeredEntity in registeredEntities)
            {
                var propertyType = registeredEntity.Type;

                services
                    .AddScoped(genericServiceType.MakeGenericType(propertyType),
                        genericServiceImplementation
                            .MakeGenericType(dbContextType, propertyType));
            }

            return services;
        }

        public static IEnumerable<IRegisteredEntity<TDbContext>> RegisterEntities<TDbContext>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
          where TDbContext : DbContext
        {
            var dbContextProvider = new DbContextProvider<TDbContext>();

            foreach (var assembly in assemblies)
            {
                var dataConfigurationTypes = assembly.GetTypes().Where(a => a.GetInterfaces().Any(a => a.FullName == typeof(IDataConfiguration<TDbContext>).FullName));
                foreach (var configurationType in dataConfigurationTypes)
                {
                    var configuration = Activator.CreateInstance(configurationType);

                    configurationType.InvokeMember(nameof(IDataConfiguration<TDbContext>.ConfigureEntities),
                        BindingFlags.InvokeMethod, Type.DefaultBinder, configuration, new object[] { dbContextProvider });
                }
            }

            return dbContextProvider.Build(services);
        }

    }
}

using DNI.Data.Core.Defaults;
using DNI.Data.Shared;
using DNI.Extensions;
using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public  static IServiceCollection Add(this IServiceCollection services, Type serviceType, Type serviceImplementationType, ServiceLifetime serviceLifetime)
        {
            services.Add(ServiceDescriptor.Describe(serviceType, serviceImplementationType, serviceLifetime));
            return services;
        }

        private static bool IsDbSet(Type type, IList<Type> genericTypesList)
        {
            var genericTypes = Array.Empty<Type>();
            var dbSetType = typeof(DbSet<>);
            if (!type.IsGenericType)
            {
                return false;
            }

            genericTypes = type.GetGenericArguments();

            genericTypesList.AddRange(genericTypes);
            var genericDbSetType = dbSetType.MakeGenericType(genericTypes?.ToArray());

            return type == genericDbSetType;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services, Type dbContextType, IEnumerable<Type> repositoryTypes, ServiceLifetime serviceLifetime)
        {
            var repositoryService = typeof(IRepository<>);
            var asyncRepositoryService = typeof(IAsyncRepository<>);
            var entityFrameworkRepositoryService = typeof(IEntityFrameworkRepository<,>);
            var repositoryImplementation = typeof(DefaultEntityFrameworkRepository<,>);

            foreach (var repositoryType in repositoryTypes)
            {
                var genericRepositoryService = repositoryService.MakeGenericType(repositoryType);
                var genericAsyncRepositoryService = asyncRepositoryService.MakeGenericType(repositoryType);
                var genericEntityFrameworkRepositoryService = entityFrameworkRepositoryService.MakeGenericType(new[] { dbContextType, repositoryType });
                var genericRepositoryImplementation = repositoryImplementation.MakeGenericType(new[] { dbContextType, repositoryType });

                services.Add(genericRepositoryService, genericRepositoryImplementation, serviceLifetime);
                services.Add(genericRepositoryService, genericEntityFrameworkRepositoryService, serviceLifetime);
                services.Add(genericAsyncRepositoryService, genericRepositoryImplementation, serviceLifetime);
            }

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services, Type dbContextType, ServiceLifetime serviceLifetime)
        {
            var genericTypeList = new List<Type>();
            var dbSetProperties = dbContextType.GetProperties().Select(p => p.PropertyType).Where(t => IsDbSet(t, genericTypeList));

            return RegisterRepositories(services, dbContextType, genericTypeList, serviceLifetime);
        }

        public static IServiceCollection RegisterRepositoriesForDbContext<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext
        {
            return services.AddDbContext<TDbContext>(optionsAction, serviceLifetime)
                .RegisterRepositories(typeof(TDbContext));
        }

        public static IServiceCollection RegisterRepositoriesForDbContext<TDbContext>(this IServiceCollection services,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime)
            where TDbContext : DbContext
        {
            return services.AddDbContext<TDbContext>(optionsAction, serviceLifetime)
                .RegisterRepositories(typeof(TDbContext));
        }

        public static IServiceCollection RegisterRepositoriesForDbContextPool<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction, int poolSize)
            where TDbContext : DbContext
        {
            return services.AddDbContextPool<TDbContext>(optionsAction, poolSize)
                .RegisterRepositories(typeof(TDbContext));
        }

        public static IServiceCollection RegisterRepositoriesForDbContextPool<TDbContext>(this IServiceCollection services,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize)
            where TDbContext : DbContext
        {
            return services.AddDbContextPool<TDbContext>(optionsAction, poolSize)
                .RegisterRepositories(typeof(TDbContext));
        }
    }
}

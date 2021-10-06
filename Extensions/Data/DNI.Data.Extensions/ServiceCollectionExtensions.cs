using DNI.Data.Core.Defaults;
using DNI.Data.Shared;
using DNI.Data.Shared.Abstractions;
using DNI.Data.Shared.Abstractions.Builders;
using DNI.Extensions;
using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace DNI.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static MethodInfo GetAddDbContextMethod(Type dbContextType, params Type[] parameterTypes)
        {
            var methods = typeof(EntityFrameworkServiceCollectionExtensions)
                .GetMethods();

            

            foreach(var method in methods.Where(a => a.Name == "AddDbContext" && a.IsStatic))
            {
                if (method.GetParameters().Select(a => a.ParameterType).SequenceEqual(parameterTypes))
                {
                    return method.MakeGenericMethod(dbContextType);
                }
            }

            return null;
        }

        public static IServiceCollection ConfigureDbContextModule(
            this IServiceCollection services,
            Action<IDbContextModuleOptionsBuilder> buildAction)
        {
            var defaultDbContextModuleOptionsBuilder = new DefaultDbContextModuleOptionsBuilder();
            buildAction(defaultDbContextModuleOptionsBuilder);

            var builtOptions = defaultDbContextModuleOptionsBuilder.Build();
            return services.AddSingleton(builtOptions);
        }

        public  static IServiceCollection Add(this IServiceCollection services, Type serviceType, 
            Type serviceImplementationType, ServiceLifetime serviceLifetime)
        {
            services.Add(ServiceDescriptor.Describe(serviceType, serviceImplementationType, serviceLifetime));
            return services;
        }

        public static IServiceCollection AddRequiredServices(this IServiceCollection services)
        {
            var subjectType = typeof(ISubject<>);

            var subjectImplementationType = typeof(Subject<>);
            return services.AddSingleton(subjectType, subjectImplementationType);
        }

        private static bool IsDbSet(this Type type, IList<Type> genericTypesList)
        {
            var dbSetType = typeof(DbSet<>);
            if (!type.IsGenericType)
            {
                return false;
            }

           var genericTypes = type.GetGenericArguments();

            genericTypesList.AddRange(genericTypes);
            var genericDbSetType = dbSetType.MakeGenericType(genericTypes?.ToArray());

            return type == genericDbSetType;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, 
            Type dbContextType, IEnumerable<Type> repositoryTypes, ServiceLifetime serviceLifetime)
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
                services.Add(genericEntityFrameworkRepositoryService, genericRepositoryImplementation, serviceLifetime);
                services.Add(genericAsyncRepositoryService, genericRepositoryImplementation, serviceLifetime);
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, Type dbContextType, ServiceLifetime serviceLifetime)
        {
            var genericTypeList = new List<Type>();

            var dbSetPropertyList = new List<Type>();
            var propertyTypes = dbContextType.GetProperties().Select(p => p.PropertyType);
            
            foreach(var propertyType in propertyTypes)
            {
                if (propertyType.IsDbSet(genericTypeList))
                {
                    dbSetPropertyList.Add(propertyType);
                }
            }

            return AddRepositories(services, dbContextType, genericTypeList, serviceLifetime);
        }

        public static IServiceCollection AddRepositoriesForDbContext(this IServiceCollection services,
            Type dbContextType, Action<DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime  = ServiceLifetime.Scoped)
            
        {
            var genericMethod = GetAddDbContextMethod(dbContextType, 
                typeof(IServiceCollection), typeof(Action<DbContextOptionsBuilder>),
                typeof(ServiceLifetime), typeof(ServiceLifetime));
            genericMethod.Invoke(services, new object [] { services, optionsAction, serviceLifetime, serviceLifetime });

            return services
                .AddRepositories(dbContextType, serviceLifetime);
        }

        public static IServiceCollection AddRepositoriesForDbContext(this IServiceCollection services,
            Type dbContextType, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            
        {
            var genericMethod = GetAddDbContextMethod(dbContextType,
                typeof(IServiceCollection), typeof(Action<IServiceProvider, DbContextOptionsBuilder>),
                typeof(ServiceLifetime), typeof(ServiceLifetime));
            genericMethod.Invoke(services, new object[] { services, optionsAction, serviceLifetime, serviceLifetime });

            return services.AddRepositories(dbContextType, serviceLifetime);
        }

        public static IServiceCollection AddRepositoriesForDbContext<TDbContext>(this IServiceCollection services,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime =  ServiceLifetime.Scoped)
            where TDbContext : DbContext
        {
            return services.AddDbContext<TDbContext>(optionsAction, serviceLifetime)
                .AddRepositories(typeof(TDbContext), serviceLifetime);
        }

        public static IServiceCollection AddRepositoriesForDbContextPool<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction, int poolSize, ServiceLifetime serviceLifetime  = ServiceLifetime.Transient)
            where TDbContext : DbContext
        {
            return services.AddDbContextPool<TDbContext>(optionsAction, poolSize)
                .AddRepositories(typeof(TDbContext), serviceLifetime);
        }

        public static IServiceCollection AddRepositoriesForDbContextPool<TDbContext>(this IServiceCollection services,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction, int poolSize, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
            where TDbContext : DbContext
        {
            return services.AddDbContextPool<TDbContext>(optionsAction, poolSize)
                .AddRepositories(typeof(TDbContext), serviceLifetime);
        }
    }
}

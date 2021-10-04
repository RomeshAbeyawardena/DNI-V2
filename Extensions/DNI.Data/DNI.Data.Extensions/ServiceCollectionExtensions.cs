﻿using DNI.Data.Core.Defaults;
using DNI.Data.Shared;
using DNI.Extensions;
using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace DNI.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public  static IServiceCollection Add(this IServiceCollection services, Type serviceType, 
            Type serviceImplementationType, ServiceLifetime serviceLifetime)
        {
            services.Add(ServiceDescriptor.Describe(serviceType, serviceImplementationType, serviceLifetime));
            return services;
        }

        public static IServiceCollection AddRequiredServices(this IServiceCollection services)
        {
            var entityEntryType = typeof(EntityEntry<>);
            var subjectType = typeof(ISubject<>);

            var subjectImplementationType = typeof(Subject<>);
            return services.AddSingleton(subjectType.MakeGenericType(entityEntryType), 
                subjectImplementationType.MakeGenericType(entityEntryType));
        }

        private static bool IsDbSet(Type type, IList<Type> genericTypesList)
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
                services.Add(genericRepositoryService, genericEntityFrameworkRepositoryService, serviceLifetime);
                services.Add(genericAsyncRepositoryService, genericRepositoryImplementation, serviceLifetime);
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, Type dbContextType, ServiceLifetime serviceLifetime)
        {
            var genericTypeList = new List<Type>();
            var dbSetProperties = dbContextType.GetProperties().Select(p => p.PropertyType).Where(t => IsDbSet(t, genericTypeList));

            return AddRepositories(services, dbContextType, genericTypeList, serviceLifetime);
        }

        public static IServiceCollection RegisterRepositoriesForDbContext<TDbContext>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime  = ServiceLifetime.Scoped)
            where TDbContext : DbContext
        {
            return services.AddDbContext<TDbContext>(optionsAction, serviceLifetime)
                .AddRepositories(typeof(TDbContext), serviceLifetime);
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

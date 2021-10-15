using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="type"></param>
        /// <param name="failIfResolvedParametersAreNull"></param>
        /// <returns></returns>
        public static object Activate(this IServiceProvider serviceProvider, Type type, out IEnumerable<IDisposable> disposables, bool failIfResolvedParametersAreNull = false)
        {
            List<IDisposable> disposableList = new();
            disposables = disposableList;
            object GetService(Type type)
            {
                var service = serviceProvider.GetService(type);
                if (failIfResolvedParametersAreNull && service == null)
                {
                    throw new ArgumentNullException(type.Name);
                }

                if (service is IDisposable disposable)
                {
                    disposableList.Add(disposable);
                }

                return service;
            }

            var service = serviceProvider.GetService(type);

            if (service != null)
            {
                return service;
            }

            var typeInterfaces = type.GetInterfaces();

            foreach (var typeInterface in typeInterfaces)
            {
                service = serviceProvider.GetService(typeInterface);
                if (service != null)
                {
                    return service;
                }
            }

            var parameterTypes = type.GetConstructorParameterTypes();
            return Activator.CreateInstance(type, parameterTypes.Select(GetService).ToArray());
        }

        public static T Activate<T>(this IServiceProvider serviceProvider, Type type, out IEnumerable<IDisposable> disposables, bool failIfResolvedParametersAreNull = false)
        {
            return (T)Activate(serviceProvider, type, out disposables, failIfResolvedParametersAreNull);
        }

        public static T Activate<T>(this IServiceProvider serviceProvider, out IEnumerable<IDisposable> disposables, bool failIfResolvedParametersAreNull = false)
        {
            return Activate<T>(serviceProvider, typeof(T), out disposables, failIfResolvedParametersAreNull);
        }
    }
}

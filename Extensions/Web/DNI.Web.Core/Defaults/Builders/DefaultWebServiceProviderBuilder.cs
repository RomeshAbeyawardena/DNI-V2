using DNI.Web.Core.Defaults.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DNI.Web.Core.Defaults.Builders
{
    public class DefaultWebServiceProviderBuilder : IServiceCollection
    {
        private readonly IServiceCollection services;
        private readonly IEnumerable<ServiceDescriptor> serviceDescriptors;
        public DefaultWebServiceProviderBuilder(IServiceCollection services, IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            this.services = services;
            this.serviceDescriptors = serviceDescriptors;
        }

        public ServiceDescriptor this[int index] { get => services[index]; set => services[index] = value; }

        public int Count => services.Count;

        public bool IsReadOnly => false;

        public void Add(ServiceDescriptor item)
        {
            services.Add(item);
        }

        public DefaultWebServiceProvider Build()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            services.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return services.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return services.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return services.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            services.Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return services.Remove(item);
        }

        public void RemoveAt(int index)
        {
            services.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return services.GetEnumerator();
        }
    }
}

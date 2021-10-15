using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModulesServiceCollection : IModulesServiceCollection
    {
        private readonly IServiceCollection services;

        public DefaultModulesServiceCollection(IServiceCollection services = default)
        {
            this.services = services ?? new ServiceCollection();
        }

        public ServiceDescriptor this[int index] { get => services[index]; set => services[index] = value; }

        public int Count => services.Count;

        public bool IsReadOnly => false;

        public event EventHandler ServiceAdded;
        public event EventHandler ServiceBuilt;

        public void OnServiceAdded(object sender, EventArgs eventArgs)
        {
            ServiceAdded?.Invoke(sender, eventArgs);
        }

        public void OnServiceBuilt(object sender, EventArgs eventArgs)
        {
            ServiceBuilt?.Invoke(sender, eventArgs);
        }

        public void Add(ServiceDescriptor item)
        {
            services.Add(item);
            OnServiceAdded(item, EventArgs.Empty);
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
            services.CopyTo(array, arrayIndex);
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

        public IServiceProvider BuildServiceProvider()
        {
            OnServiceBuilt(this, EventArgs.Empty);
            return services.BuildServiceProvider();
        }
    }
}

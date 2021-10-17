using System;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultFakeServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return null;
        }
    }
}

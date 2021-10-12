using System;

namespace DNI.Modules.Shared.Abstractions
{
    /// <summary>
    /// Defines a mechanism for retrieving a service object; that is, an object that
    /// provides custom support to other objects, based off a parent and child <see cref="IServiceProvider"/>
    /// </summary>
    public interface IModuleServiceProvider : IServiceProvider
    {

    }
}

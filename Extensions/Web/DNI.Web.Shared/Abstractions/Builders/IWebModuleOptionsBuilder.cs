﻿using DNI.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DNI.Web.Shared.Abstractions.Builders
{
    public interface IWebModuleOptionsBuilder : IAssemblyOptionsBuilder
    {
        new IWebModuleOptionsBuilder AddAssembly<T>();
        new IWebModuleOptionsBuilder AddAssembly(Type type);
        new IWebModuleOptionsBuilder AddAssembly(Assembly assembly);
        IWebModuleOptionsBuilder ConfigureWebHost(Action<IWebHostBuilder> configureAction);
        IWebModuleOptionsBuilder ConfigureMvcOptions(Action<IMvcBuilder> mvcOptions);
        IWebModuleOptionsBuilder UseDefaultMvcOptions();
        IWebModuleOptionsBuilder AddModuleAssemblies();
        IWebModuleOptions Build(Assembly hostAssembly);
    }
}

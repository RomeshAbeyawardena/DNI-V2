﻿using DNI.Mediator.Core.Defaults;
using DNI.Mediator.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Mediator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMediatorModule(
            this IServiceCollection services,
            Action<IMediatorModuleOptionsBuilder> buildAction)
        {
            IMediatorModuleOptionsBuilder defaultMediatorModuleOptionsBuilder = new DefaultMediatorModuleOptionsBuilder();
            buildAction?.Invoke(defaultMediatorModuleOptionsBuilder);
            return services.AddSingleton(defaultMediatorModuleOptionsBuilder.Build());
        }
    }
}

using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Modules
{
    public abstract class WebAppModule<TAppModule> : AppModuleBase<WebAppModule<TAppModule>>
        where TAppModule : class, IAppModule
    {
        private readonly IAppModuleConfig<WebAppModule<TAppModule>> appModuleConfig;

        public WebAppModule(IAppModuleCache<WebAppModule<TAppModule>> appModuleCache,
            IAppModuleConfig<WebAppModule<TAppModule>> appModuleConfig)
            : base(appModuleCache)
        {
            this.appModuleConfig = appModuleConfig;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            return CreateHostBuilder(null)
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<TAppModule>()
                .ConfigureServices(services => RegisterServices(AppModuleCache, appModuleConfig, services));
            }).RunConsoleAsync();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public static void RegisterServices(IAppModuleCache appModuleCache, IAppModuleConfig<WebAppModule<TAppModule>> appModuleConfig, IServiceCollection services)
        {
            services
                .AddControllers();
        }

        public override bool ValidateServices(IServiceProvider serviceProvider)
        {
            return true;
        }

        public abstract IServiceProvider ConfigureServices(IServiceCollection services);
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

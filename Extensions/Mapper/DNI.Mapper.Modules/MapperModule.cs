using DNI.Extensions;
using DNI.Mapper.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Mapper.Modules
{
    public class MapperModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var mapperOptions = moduleConfiguration.GetOptions<IMapperOptions>(ModuleDescriptor);

            var assemblies = mapperOptions.Assemblies;

            if (mapperOptions.UseModuleAssemblies)
            {
                assemblies = assemblies.AppendMany(moduleConfiguration.GetModuleAssemblies());
            }

            services.AddAutoMapper(assemblies);
        }
    }
}

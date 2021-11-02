using DNI.Mapper.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Mapper.Modules
{
    public class MapperModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var mapperOptions = moduleConfiguration.GetOptions<IMapperOptions>(ModuleDescriptor);

            List<Assembly> assemblyList = new();
            if (mapperOptions.Any())
            {
                assemblyList.AddRange(mapperOptions);
            }

            if (mapperOptions.UseModuleAssemblies)
            {
                assemblyList.AddRange(moduleConfiguration.GetModuleAssemblies());
            }

            services.AddAutoMapper(assemblyList);
        }
    }
}

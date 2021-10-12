using DNI.Shared.Abstractions;
using DNI.Shared.Defaults;
using System;

namespace DNI.Shared
{
    public static class AssemblyOptions
    {

        public const Enumerations.AssemblyOptions Startup = Enumerations.AssemblyOptions.Discoverable | Enumerations.AssemblyOptions.OnStartup;
        public const Enumerations.AssemblyOptions Injectable = Enumerations.AssemblyOptions.Discoverable | Enumerations.AssemblyOptions.Injectable;


        public static void SetAssemblyOptions(this IAssemblyOptions options, Enumerations.AssemblyOptions assemblyOptions)
        {
            if (assemblyOptions.HasFlag(Enumerations.AssemblyOptions.Discoverable))
            {
                options.Discoverable = true;
            }

            if (assemblyOptions.HasFlag(Enumerations.AssemblyOptions.Injectable))
            {
                options.Injectable = true;
            }

            if (assemblyOptions.HasFlag(Enumerations.AssemblyOptions.OnStartup))
            {
                options.OnStartup = true;
            }

        }

        public static IAssemblyOptions GetAssemblyOptions(Enumerations.AssemblyOptions assemblyOptions)
        {
            var options = new DefaultAssemblyOptions();
            SetAssemblyOptions(options, assemblyOptions);
            return options;
        }
    }
}

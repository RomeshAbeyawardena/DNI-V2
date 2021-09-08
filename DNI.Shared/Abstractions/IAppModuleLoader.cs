using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    /// <summary>
    /// Represents a loader of <see cref="IAppModule">modules</see>
    /// </summary>
    public interface IAppModuleLoader : IDisposable
    {
        /// <summary>
        /// Loads modules from a json file
        /// </summary>
        /// <param name="moduleJsonPath">Path of the json file containing modules to load</param>
        /// <param name="assemblyPaths"><c>Optional:</c> Assembly paths to include in the search</param>
        void Load(string moduleJsonPath, params string[] assemblyPaths);
        /// <summary>
        /// Loads modules from a json string
        /// </summary>
        /// <param name="json">Valid JSON string containing a list of modules to load</param>
        /// <param name="options">Module loader options to be used for loading modules</param>
        void Load(string json, IAppModulesLoaderOptions options);

        /// <summary>
        /// Gets the modules loading into memory
        /// </summary>
        IEnumerable<IAppModule> Modules { get; }

        /// <summary>
        /// Gets the loaded assemblies
        /// </summary>
        IEnumerable<Assembly> LoadedAssemblies { get; }
    }
}

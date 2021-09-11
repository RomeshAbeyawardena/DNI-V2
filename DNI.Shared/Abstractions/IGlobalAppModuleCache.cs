namespace DNI.Shared.Abstractions
{
    public interface IGlobalAppModuleCache<TAppModuleLoader> : IAppModuleCache
        where TAppModuleLoader : class, IAppModuleLoader
    {
        /// <summary>
        /// Registers an <see cref="IAppModule">app module</see> that contains a list of services and infrastructure that is required by another <see cref="IAppModule"/> or globally to be used by all <see cref="IAppModule">modules</see>
        /// </summary>
        /// <typeparam name="TAppModule"></typeparam>
        /// <returns></returns>
        IGlobalAppModuleCache<TAppModuleLoader> RegisterModule<TAppModule>(IConfig config = null)
            where TAppModule : IAppModule;
    }
}
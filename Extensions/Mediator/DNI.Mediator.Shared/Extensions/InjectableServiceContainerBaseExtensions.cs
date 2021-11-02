using DNI.Mediator.Shared.Base;

namespace DNI.Mediator.Shared.Extensions
{
    public static class InjectableServiceContainerBaseExtensions
    {
        public static T GetService<T>(this InjectableServiceContainerBase injectableServiceContainer)
        {
            return (T)injectableServiceContainer.GetService(typeof(T));
        }
    }
}

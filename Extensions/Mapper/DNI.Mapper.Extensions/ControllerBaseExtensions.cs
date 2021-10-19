using AutoMapper;
using DNI.Web.Shared.Base;

namespace DNI.Mapper.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static TDestination Map<TDestination>(this ControllerBase controllerBase, object value)
        {
            var mapper = controllerBase.GetService<IMapper>();

            return mapper.Map<TDestination>(value);
        }
    }
}

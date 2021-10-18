using AutoMapper;
using DNI.Web.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

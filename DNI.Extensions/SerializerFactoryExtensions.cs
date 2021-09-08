using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class SerializerFactoryExtensions
    {
        public static string Serialize<T>(this ISerializerFactory serializerFactory, SerializerType serializerType, T model)
        {
            return GetSerializer(serializerFactory, serializerType)
                .Serialize(model);
        }
        public static T Deserialize<T>(this ISerializerFactory serializerFactory, SerializerType serializerType, string plainText)
        {
            return GetSerializer(serializerFactory, serializerType)
                .Deserialize<T>(plainText);
        }

        private static ISerializer GetSerializer(ISerializerFactory serializerFactory, SerializerType serializerType)
        {
            return serializerFactory.GetSerializer(serializerType);
        }
    }
}

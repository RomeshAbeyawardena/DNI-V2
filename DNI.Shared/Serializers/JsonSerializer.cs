using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerializerProvider = System.Text.Json.JsonSerializer;

namespace DNI.Shared.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public SerializerType Type => SerializerType.Json;

        public T Deserialize<T>(string plainText)
        {
            return SerializerProvider.Deserialize<T>(plainText);
        }

        public string Serialize<T>(T model)
        {
            return SerializerProvider.Serialize(model);
        }
    }
}

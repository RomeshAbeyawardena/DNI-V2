using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Factory
{
    public class SerializerFactory : ISerializerFactory
    {
        private readonly IEnumerable<ISerializer> serializers;

        public SerializerFactory(IEnumerable<ISerializer> serializers)
        {
            this.serializers = serializers;
        }

        public ISerializer GetSerializer(SerializerType serializerType)
        {
            return serializers.FirstOrDefault(a => a.Type == serializerType);
        }
    }
}

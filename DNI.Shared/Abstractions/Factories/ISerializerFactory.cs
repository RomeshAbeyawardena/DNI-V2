using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Factories
{
    public interface ISerializerFactory
    {
        ISerializer GetSerializer(SerializerType serializerType);
    }
}

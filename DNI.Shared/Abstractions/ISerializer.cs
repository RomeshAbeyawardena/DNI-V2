using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface ISerializer
    {
        SerializerType Type { get; }

        string Serialize<T>(T model);
        T Deserialize<T>(string plainText);
    }
}

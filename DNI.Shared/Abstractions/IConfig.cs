using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IConfig : IDictionary<string, object>
    {
        void Load(IFile file, SerializerType serializerType);
    }
}

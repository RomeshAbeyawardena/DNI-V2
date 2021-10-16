using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Shared.Abstractions
{
    public interface IModelEncryptor
    {
        T Encrypt<T>(T model);
        T Decrypt<T>(T model);
    }
}

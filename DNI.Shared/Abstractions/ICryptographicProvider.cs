using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface ICryptographicProvider
    {
        IEncryptionOptions EncryptionOptions { get; }
    }
}

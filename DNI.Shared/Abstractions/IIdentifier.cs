using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IIdentifier
    {
        object Id { get; }
    }

    public interface IIdentifier<TKey> : IIdentifier
    {
        new TKey Id { get; }
    }
}

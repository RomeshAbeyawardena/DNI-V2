using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IKeyValuePair<TKey, TValue>
    {
        KeyValuePair<TKey, TValue> KeyValuePair { get; }
        TKey Key { get;  }
        TValue Value { get; }
    }
}

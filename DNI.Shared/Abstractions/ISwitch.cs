using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface ISwitch<TKey, TValue> : IDictionary<TKey, TValue>
    {
        ISwitch<TKey, TValue> CaseWhen(TKey key, TValue value);
        TValue Case(TKey key);
    }
}

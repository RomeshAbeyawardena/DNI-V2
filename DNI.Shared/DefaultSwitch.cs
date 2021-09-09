using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared
{
    public class DefaultSwitch<TKey, TValue> : DictionaryBase<TKey, TValue>, ISwitch<TKey, TValue>
    {
        public TValue Case(TKey key)
        {
            if(TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public ISwitch<TKey, TValue> CaseWhen(TKey key, TValue value)
        {
            Add(key, value);
            return this;
        }
    }
}

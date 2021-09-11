using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared
{
    public static class DictionaryBuilder 
    {
        public static IDictionaryBuilder<TKey, TValue> Build<TKey, TValue>(Action<IDictionaryBuilder<TKey, TValue>> build)
        {
            var dictionaryBuilder = new DictionaryBuilder<TKey, TValue>();
            build?.Invoke(dictionaryBuilder);
            return dictionaryBuilder;
        }
    }

    public class DictionaryBuilder<TKey, TValue> : DictionaryBase<TKey, TValue>, IDictionaryBuilder<TKey, TValue>
    {
        IDictionaryBuilder<TKey, TValue> IDictionaryBuilder<TKey, TValue>.Add(TKey key, TValue value)
        {
            Add(key, value);
            return this;
        }
    }
}

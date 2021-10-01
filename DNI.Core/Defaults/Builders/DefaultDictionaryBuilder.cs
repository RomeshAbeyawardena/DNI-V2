using DNI.Shared.Abstractions.Builders;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults.Builders
{
    public class DefaultDictionaryBuilder<TKey, TValue> : DictionaryBase<TKey, TValue>, IDictionaryBuilder<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Dictionary => base.dictionary;
    }
}

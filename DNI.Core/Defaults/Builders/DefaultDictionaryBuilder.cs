﻿using DNI.Shared.Abstractions;
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

        public IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair)
        {
            return this.Add(keyValuePair.KeyValuePair);
        }

        public new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value)
        {
            base.Add(key, value);
            return this;
        }

        public new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return this.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Builders
{
    /// <summary>
    /// Represents a dictionary builder to build a dictionary using 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IDictionaryBuilder<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Gets the produced <see cref="IDictionary{TKey, TValue}"/> instance
        /// </summary>
        IDictionary<TKey, TValue> Dictionary { get; }

        /// <summary>Adds an element with the provided key and value to the <see cref="IDictionary{TKey, TValue}"/></summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);

        /// <summary>Adds an element with the provided key and value to the <see cref="IDictionary{TKey, TValue}"/></summary>
        /// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}"/> to add</param>
        IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair);

        /// <summary>Adds an element with the provided key and value to the <see cref="IDictionary{TKey, TValue}"/></summary>
        /// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}"/> to add</param>
        new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair);
    }
}

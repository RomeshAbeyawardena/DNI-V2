using DNI.Shared.Abstractions.Builders;
using DNI.Shared.Base;
using System.Collections.Generic;

namespace DNI.Core.Defaults.Builders
{
    public class DefaultListBuilder<T> : CollectionBase<T>, IListBuilder<T>
    {
        IListBuilder<T> IListBuilder<T>.AddRange(IEnumerable<T> items)
        {
            base.AddRange(items);
            return this;
        }

        IListBuilder<T> IListBuilder<T>.Add(T item)
        {
            base.Add(item);
            return this;
        }
    }
}

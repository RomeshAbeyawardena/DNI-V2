using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class IdentifierBase<TKey> : IIdentifier<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; }

        object IIdentifier.Id => Id;
    }
}

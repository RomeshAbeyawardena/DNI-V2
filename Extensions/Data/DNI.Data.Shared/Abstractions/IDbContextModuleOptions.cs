using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Shared.Abstractions
{
    public interface IDbContextModuleOptions
    {
        IEnumerable<IDbContextTypeOptions> DbContextTypeOptions { get; }
    }
}

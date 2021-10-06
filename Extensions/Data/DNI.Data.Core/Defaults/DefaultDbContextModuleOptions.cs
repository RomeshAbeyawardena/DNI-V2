using DNI.Data.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Core.Defaults
{
    public class DefaultDbContextModuleOptions : IDbContextModuleOptions
    {
        public IEnumerable<IDbContextTypeOptions> DbContextTypeOptions { get; }

        public DefaultDbContextModuleOptions(IEnumerable<IDbContextTypeOptions> dbContextTypeOptions)
        {
            DbContextTypeOptions = dbContextTypeOptions;
        }
    }
}

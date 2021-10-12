using DNI.Data.Shared.Abstractions;
using System.Collections.Generic;

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

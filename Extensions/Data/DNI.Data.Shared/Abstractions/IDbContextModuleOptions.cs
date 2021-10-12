using System.Collections.Generic;

namespace DNI.Data.Shared.Abstractions
{
    public interface IDbContextModuleOptions
    {
        IEnumerable<IDbContextTypeOptions> DbContextTypeOptions { get; }
    }
}

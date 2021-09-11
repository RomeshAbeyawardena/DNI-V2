using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database.Abstractions
{
    public interface IEntityFrameworkAppConfig : IConfig
    {
        string ConnectionString { get; }
    }
}

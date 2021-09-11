using DNI.Modules.Database;
using DNI.Modules.Database.Abstractions;
using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Sandbox
{
    public class SandboxDatabaseAppModule : EntityFrameworkAppModule<SandboxDbContext>
    {
        public SandboxDatabaseAppModule(IAppModuleCache<EntityFrameworkAppModule<SandboxDbContext>> appModuleCache, IEntityFrameworkAppConfig config) : base(appModuleCache, config)
        {
        }
    }

    public class SandboxDbContext : DbContext
    {

    }
}

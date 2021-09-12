using DNI.Modules.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Sandbox.Entities
{
    public class Configuration : IDataConfiguration<SandboxDbContext>
    {
        public void ConfigureEntities(IDbContextProvider<SandboxDbContext> dbContextProvider)
        {
            dbContextProvider
                .Using(typeof(Configuration).Assembly)
                .RegisterEntitiesWithEntityAttribute();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database.Abstractions
{
    public interface IDataConfiguration<TDbContext>
        where TDbContext : DbContext
    {
        public void ConfigureEntities(IDbContextProvider<TDbContext> dbContextProvider);
    }
}

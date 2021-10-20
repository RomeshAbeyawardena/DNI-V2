using DNI.Data.Shared.Base;
using DNI.Shared.Test;
using Microsoft.EntityFrameworkCore;

namespace DNI.Test.App
{
    class MyDbContext : DbContextBase
    {
        public MyDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

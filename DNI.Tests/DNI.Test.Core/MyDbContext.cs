using DNI.Data.Shared.Base;
using DNI.Tests.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DNI.Test.Core
{
    public class MyDbContext : DbContextBase
    {
        public MyDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}

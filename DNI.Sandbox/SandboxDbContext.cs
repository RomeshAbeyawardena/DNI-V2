using DNI.Sandbox.Entities;
using Microsoft.EntityFrameworkCore;

namespace DNI.Sandbox
{
    public class SandboxDbContext : DbContext
    {
        public SandboxDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}

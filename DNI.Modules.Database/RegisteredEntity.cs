using DNI.Modules.Database.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database
{
    public class RegisteredEntity<TDbContext> : IRegisteredEntity<TDbContext>
        where TDbContext : DbContext
    {
        public static IRegisteredEntity<TDbContext> Register<TEntity>()
          where TEntity : class
        {
            return new RegisteredEntity<TDbContext>(typeof(TEntity));
        }

        public RegisteredEntity(Type type)
        {
            Type = type;
        }
        public Type Type { get; }
    }
}

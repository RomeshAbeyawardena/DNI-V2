using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Shared.Base
{
    public abstract class DbContextBase : DbContext
    {
        private EntityEntry<TEntity> InformOfChange<TEntity>(EntityEntry<TEntity> entityEntry)
            where TEntity : class
        {
            GetSubject<TEntity>()?.OnNext(entityEntry);
            return entityEntry;
        }

        protected ISubject<EntityEntry<T>> GetSubject<T>()
            where T : class
        {
            return this.GetService<ISubject<EntityEntry<T>>>();
        }

        public DbContextBase(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return InformOfChange(base.Add(entity));
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            return InformOfChange(base.Update(entity));
        }

    }
}

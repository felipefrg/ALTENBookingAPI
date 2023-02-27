using ALTENBooking.Data.Context;
using ALTENBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Data
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly EFInMemoryDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(EFInMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            entity.Active = false;
            _entities.Update(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            var entities = _entities.AsQueryable();
            if(filter != null)
            {
                entities = entities.Where(filter);
            }
            return entities;    
        }

        public T? GetById(Guid id)
        {
            return _entities.Find(id);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;            
            _entities.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll(Expression<Func<T,bool>> filter = null);
    }
}

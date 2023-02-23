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
        T GetById(Guid id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        IQueryable<T> GetAll(Expression<Func<T,bool>> filter = null);
    }
}

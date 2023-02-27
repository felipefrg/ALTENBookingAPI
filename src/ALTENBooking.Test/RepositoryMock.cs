using ALTENBooking.Domain.Interfaces;
using System.Linq.Expressions;

namespace ALTENBooking.Test
{
    internal class RepositoryMock<T> : IRepository<T> where T : class, IBaseEntity
    {
        List<T> reservationList = new List<T>();

        public void Add(T entity)
        {
            reservationList.Add(entity);
        }

        public void Delete(T entity)
        {
            reservationList.Remove(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return reservationList.Where(filter.Compile());
            else
                return reservationList;
        }

        public T? GetById(Guid id)
        {
            return reservationList.Where(p => p.Id == id).FirstOrDefault();
        }

        public void Update(T entity)
        {
            var dbEntity = reservationList.Where(p => p.Id == entity.Id).FirstOrDefault();
            if (dbEntity != null)
            {
                var properties = dbEntity.GetType().GetProperties();
                foreach(var property in properties)
                {
                    dbEntity.GetType().GetProperty(property.Name)!
                        .SetValue(entity, property.GetValue(entity, null), null);
                }
                dbEntity.UpdatedAt = DateTime.Now;
            }
        }
    }
}

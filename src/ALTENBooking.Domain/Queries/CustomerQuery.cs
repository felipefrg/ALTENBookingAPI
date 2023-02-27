using ALTENBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Queries
{
    public static class CustomerQuery
    {
        public static Expression<Func<Customer,bool>> GetByEmail(string email)
        {
            return x => x.Email == email;
                     
        }      
    }
}

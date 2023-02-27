using ALTENBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Interfaces
{
    public interface ICustomerService
    {
        Result Create(Customer customer);         
        Customer GetByEmail(string email);
        Customer Get(Guid id);
    }
}

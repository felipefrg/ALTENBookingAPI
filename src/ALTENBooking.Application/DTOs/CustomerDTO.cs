using ALTENBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Application.DTOs
{
    public class CustomerDTO
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;        
        public DateTime DateOfBirth { get; set; }

        public static CustomerDTO FromModel(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Document = customer.Document,
                DateOfBirth = customer.DateOfBirth.Value
            };

        }

        public Customer ToModel()
        {
            return new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Document = Document,
                DateOfBirth = DateOfBirth
            };
        }
    }
}

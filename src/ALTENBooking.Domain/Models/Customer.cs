using ALTENBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Models
{
    public class Customer : BaseEntity
    {        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }  = string.Empty;        
        public string Email { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public Genre Genre { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain
{
    public class BookingException : Exception
    {
        new public string Message { get; set; }

        public BookingException(string messageError)
        {
            this.Message = messageError;
        }
    }
}

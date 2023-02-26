using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Models
{
    public class Result
    {
        public bool HasError { get; set; }
        public string? Message { get; set; }
        
    }
    public class Result<T> : Result where T : class
    {
        public T? Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Application.DTOs
{
    public class ResultDTO
    {
        public bool HasError { get; set; }
        public string Message { get; set; }        
    }

    public class ResultDTO<T> : ResultDTO where T : class
    {        
        public T Data { get; set; }
    }
}

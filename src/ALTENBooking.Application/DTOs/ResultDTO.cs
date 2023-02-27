using ALTENBooking.Domain.Models;
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

        public static ResultDTO FromModel(Result result)
        {
            return new ResultDTO
            {
                HasError = result.HasError
                ,
                Message = result.Message
            };
        }
    }
}

using ALTENBooking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Test
{
    internal class CurrentDateTimeMock : ICurrentDateTime
    {
        public DateTime Value { get; set; }

        public CurrentDateTimeMock(DateTime value)
        {
            Value = value;  
        }
    }
}

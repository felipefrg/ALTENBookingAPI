using ALTENBooking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Services
{
    public class CurrentDateTime : ICurrentDateTime
    {
        public DateTime Value { get; private set; } = DateTime.Now;
    }
}

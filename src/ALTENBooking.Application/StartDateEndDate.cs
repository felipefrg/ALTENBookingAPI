using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Application
{
    internal class StartDateEndDate
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public StartDateEndDate(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate.Date;
            EndDate = endDate.Date.AddDays(1).AddMilliseconds(-1);
        }
    }
}

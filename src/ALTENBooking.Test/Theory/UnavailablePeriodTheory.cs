using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Test.Theory
{
    internal class UnavailablePeriodTheory : TheoryData<DateTime,DateTime>
    {
        public UnavailablePeriodTheory()
        {
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 26, 23, 59, 59));
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 24, 23, 59, 59));
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 25, 23, 59, 59));
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 28, 23, 59, 59));
            Add(new DateTime(2023, 02, 25), new DateTime(2023, 02, 28, 23, 59, 59));
            Add(new DateTime(2023, 02, 26), new DateTime(2023, 02, 28, 23, 59, 59));    
        }
    }

    internal class UnavailableValidPeriodTheory : TheoryData<DateTime, DateTime>
    {
        public UnavailableValidPeriodTheory()
        {
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 26, 23, 59, 59));
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 24, 23, 59, 59));
            Add(new DateTime(2023, 02, 24), new DateTime(2023, 02, 25, 23, 59, 59));          
        }
    }
}

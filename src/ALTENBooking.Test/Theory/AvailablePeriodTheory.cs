using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Test.Theory
{
    internal class AvailablePeriodTheory : TheoryData<DateTime,DateTime>
    {
        public AvailablePeriodTheory()
        {
            Add(new DateTime(2023, 02, 27), new DateTime(2023, 02, 28, 23, 59, 59));
            Add(new DateTime(2023, 02, 27), new DateTime(2023, 03, 01, 23, 59, 59));
            Add(new DateTime(2023, 02, 28), new DateTime(2023, 03, 10, 23, 59, 59));
        }
    }
}

using ALTENBooking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Application.Interfaces
{
    public interface IBookingService
    {
        bool IsRoomAvailable(DateTime startDate, DateTime endDate);

        ResultDTO DoReservation(BookingDTO createBooking);

        ResultDTO CancelReservation(Guid customerId);

        ResultDTO ModifyReservation(BookingUpdateDTO createBooking);

        IList<BookingDTO> GetReservation();
    }
}

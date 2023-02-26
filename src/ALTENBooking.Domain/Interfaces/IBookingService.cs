using ALTENBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Interfaces
{
    public interface IBookingService
    {
        bool HasAnyRoomAvailable(DateTime startDate, DateTime endDate);

        Result DoReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate);

        Result CancelReservation(Guid CustomerId);

        Result ModifyReservation(Guid ReservationId, Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate);
    }
}

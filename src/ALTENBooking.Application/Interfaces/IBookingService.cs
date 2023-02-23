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

        ResultDTO DoReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate);

        ResultDTO CancelReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate);

        ResultDTO ModifyReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate);
    }
}

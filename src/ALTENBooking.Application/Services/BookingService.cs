using ALTENBooking.Application.DTOs;
using ALTENBooking.Application.Interfaces;
using ALTENBooking.Domain.Models;

namespace ALTENBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        
        public BookingService()
        {
           
        }
        public ResultDTO CancelReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public ResultDTO DoReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public bool IsRoomAvailable(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public ResultDTO ModifyReservation(Guid CustomerId, Guid RoomId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}

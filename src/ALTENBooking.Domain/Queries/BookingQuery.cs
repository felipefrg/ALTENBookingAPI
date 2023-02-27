using ALTENBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Queries
{
    public static class BookingQuery
    {
        public static Expression<Func<Reservation,bool>> RoomAvailability(DateTime startDate, DateTime endDate, Guid? roomId = null)
        {
            return x => 
                     (roomId == null || x.RoomId == roomId.Value)
                     &&
                     x.StartDate >= startDate
                     && x.StartDate < endDate
                     || 
                     x.EndDate > startDate
                     && x.EndDate <= endDate;
        }

        public static Expression<Func<Reservation, bool>> CustomerAlreadyHasValidReservation(Guid customerId,Guid roomId,DateTime startDate)
        {
            return x => x.StartDate >= startDate
                     && x.CustomerId == customerId
                     && x.RoomId == roomId;
                     
        }

        public static Expression<Func<Reservation, bool>> GetReservationByCustomerId(Guid customerId)
        {
            return x => x.CustomerId == customerId;
        }

        public static Expression<Func<Reservation, bool>> GetReservationById(Guid id)
        {
            return x => x.Id == id;
        }

        public static Expression<Func<Reservation, bool>> GetRoomByDate(DateTime startDate, DateTime endDate)
        {
            return x =>                    
                     x.StartDate == startDate                     
                     && x.EndDate == endDate;
        }

        public static Expression<Func<Room, bool>> GetRoomById(Guid Id)
        {
            return x => x.Id == Id;
                     
        }
    }
}

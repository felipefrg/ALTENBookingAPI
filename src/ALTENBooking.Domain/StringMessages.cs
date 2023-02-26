using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain
{
    public static class StringMessages
    {
       public readonly static string Reservation30DaysErrorMessage
            = "Reservation day is great than 30 days";

        public readonly static string ReservationStartDateGreatThanEndDateErrorMessage
           = "Start date great than end date";

        public readonly static string ReservationStayGreatThan3DaysInRowErrorMessage
          = "Stay more than 3 days in a row is not allowed";

        public readonly static string ReservationStartDateMinorThanCurrentDate
          = "Start date minor than current date";

        public readonly static string ReservationUserAlreadyBookedRoom
          = "Customer already has a valid reservation";

        public readonly static string ReservationRoomUnavailable
          = "Room is not avalaible on this date";

        public readonly static string ReservationNotFound
          = "Reservation not found";
    }
}

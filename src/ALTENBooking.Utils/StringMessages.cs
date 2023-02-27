using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Utils
{
    public static class StringMessages
    {
       public readonly static string Reservation30DaysErrorMessage
            = "Reservation day is greater than 30 days";

        public readonly static string ReservationStartDateGreatThanEndDateErrorMessage
           = "Start date greater than end date";

        public readonly static string ReservationStayGreatThan3DaysInRowErrorMessage
          = "Stay more than 3 days in a row is not allowed";

        public readonly static string ReservationStartDateMinorThanCurrentDate
          = "Start date minor than current date";

        public readonly static string ReservationUserAlreadyBookedRoom
          = "Customer already has a valid reservation";

        public readonly static string ReservationRoomUnavailable
          = "There isn't room avalaible on this date";

        public readonly static string ReservationRoomAvailable
          = "There is room avalaible on this date";

        public readonly static string ReservationNotFound
          = "Reservation not found";

        public readonly static string ErrorOccurred
          = "An error has occurred";

        public readonly static string UnexpectedError
          = "An unexpected error has occurred";

        public readonly static string CustomerEmailAlreadyExists
         = "Email has already been used";

        public readonly static string PropertyEmpty
         = "Property {0} can not be empty";

        public readonly static string ReservationCreateOK
         = "Booking has been done";

        public readonly static string ReservationModificateOK
         = "Booking has been updated";

        public readonly static string ReservationDeleteOK
         = "Booking has been canceled";

        public readonly static string RoomNotExists
        = "Room not exists";

        public readonly static string BookingIdNotNull
        = "Reservation Id cannot be null";

        public readonly static string CustomerIdNotNull
        = "Customer Id cannot be null";
    }
}

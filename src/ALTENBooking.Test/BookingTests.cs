namespace ALTENBooking.Test
{
    public class BookingTests
    {
        [Fact]
        public void GivenBookingRoom_WhenDateUnavailable_ThenReturnRoomUnavailable()
        {

        }

        [Fact]
        public void GivenBookingRoom_WhenDateAvailable_ThenReturnRoomAvailable()
        {

        }

        [Fact]
        public void GivenBookingRoom_WhenDateLessThanCurrentDate_ThenReturnBookingError()
        {

        }

        [Fact]
        public void GivenBookingRoom_WhenDateGreaterThan30Days_ThenReturnRoomUnavailable()
        {

        }

        [Fact]
        public void GivenBookingRoom_WhenStayGreaterThan3Days_ThenReturnBookingError()
        {

        }

        [Fact]
        public void GivenBookingRoom_WhenNumberDayInvalid_ThenReturnBookingError()
        {

        }

    }
}
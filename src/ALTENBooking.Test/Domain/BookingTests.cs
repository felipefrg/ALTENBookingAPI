using ALTENBooking.Domain.Services;
using ALTENBooking.Domain.Models;
using ALTENBooking.Test.Theory;
using ALTENBooking.Domain;
using ALTENBooking.Domain.Enums;
using ALTENBooking.Domain.Queries;

namespace ALTENBooking.Test.Domain
{ 
    public class BookingTests
    {
        RepositoryMock<Reservation> _reservationRepositoryMock;
        RepositoryMock<Customer> _customerRepositoryMock;
        RepositoryMock<Room> _roomRepositoryMock;
        Guid _customerId = Guid.NewGuid();
        Guid _roomId = Guid.NewGuid();

        public BookingTests()
        {            
            SetupCustomer();
            SetupRoom();
            SetupReservation();
        }

        [Theory]
        [ClassData(typeof(UnavailablePeriodTheory))]
        public void GivenBookingRoom_WhenDateUnavailable_ThenReturnRoomUnavailable(DateTime startDate, DateTime endDate)
        {
            //arrange
            BookingService bookingService = BuildBookingService();
            //act
            bool isAvailable = bookingService.HasAnyRoomAvailable(startDate, endDate);

            //assert
            Assert.False(isAvailable);
        }

        [Theory]
        [ClassData(typeof(AvailablePeriodTheory))]
        public void GivenBookingRoom_WhenDateAvailable_ThenReturnRoomAvailable(DateTime startDate, DateTime endDate)
        {
            //arrange
            BookingService bookingService = BuildBookingService();

            //act
            bool isAvailable = bookingService.HasAnyRoomAvailable(startDate, endDate);

            //assert
            Assert.True(isAvailable);
        }

        [Fact]
        public void GivenBookingRoom_WhenDateGreaterThan30Days_ThenReturnReservationError()
        {
            //arrange
            var startDate = new DateTime(2023, 04, 01);
            var endDate = new DateTime(2023, 04, 02);
            BookingService bookingService = BuildBookingService();

            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService.DoReservation(_customerId, _roomId, startDate, endDate);
            });

            //assert
            Assert.Equal(StringMessages.Reservation30DaysErrorMessage, exception.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenStartDateGreaterThanEndDate_ThenReturnReservationError()
        {
            //arrange
            var startDate = new DateTime(2023, 02, 27);
            var endDate = new DateTime(2023, 02, 26);

            BookingService bookingService = BuildBookingService();
            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService.DoReservation(_customerId, _roomId, startDate, endDate);
            });

            //assert            
            Assert.Equal(StringMessages.ReservationStartDateGreatThanEndDateErrorMessage, exception.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenStayGreaterThan3DaysInRow_ThenReturnBookingError()
        {
            //arrange
            var startDate = new DateTime(2023, 03, 4);
            var endDate = new DateTime(2023, 03, 10);
            BookingService bookingService = BuildBookingService();

            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService.DoReservation(_customerId, _roomId, startDate, endDate);
            });

            //assert            
            Assert.Equal(StringMessages.ReservationStayGreatThan3DaysInRowErrorMessage, exception.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenStartDateLessThanCurrentDate_ThenReturnBookingError()
        {
            //arrange
            var startDate = new DateTime(2023, 03, 4);
            var endDate = new DateTime(2023, 03, 10);
            BookingService bookingService = BuildBookingService();

            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService.DoReservation(_customerId, _roomId, startDate, endDate);
            });

            //assert            
            Assert.Equal(StringMessages.ReservationStayGreatThan3DaysInRowErrorMessage, exception.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenUserAlreadyHaValidReservation_ThenReturnBookingError()
        {
            //arrange
            var startDate = new DateTime(2023, 03, 4);
            var endDate = new DateTime(2023, 03, 5);
            BookingService bookingService = BuildBookingService();

            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService.DoReservation(_customerId, _roomId, startDate, endDate);
            });

            //assert            
            Assert.Equal(StringMessages.ReservationUserAlreadyBookedRoom, exception.Message);
        }

        [Theory]
        [ClassData(typeof(UnavailableValidPeriodTheory))]
        public void GivenBookingRoom_WhenRoomIsNotAvailable_ThenReturnBookingError(DateTime startDate, DateTime endDate)
        {
            //arrange           
            BookingService bookingService = BuildBookingService();
            Guid customerId = Guid.NewGuid();

            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService.DoReservation(customerId, _roomId, startDate, endDate);
            });

            //assert            
            Assert.Equal(StringMessages.ReservationRoomUnavailable, exception.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenCancelBooking_ThenReturnBookingCanceled()
        {
            //arrange           
            BookingService bookingService = BuildBookingService();
            Guid customerId = Guid.NewGuid();
            var startDate = new DateTime(2023, 03, 7);
            var endDate = startDate.AddDays(3).AddMilliseconds(-1);            
            bookingService.DoReservation(customerId, _roomId, startDate, endDate);

            //act
            Result result = bookingService.CancelReservation(customerId);
            var reservation = _reservationRepositoryMock
                .GetAll(BookingQuery.GetReservationByCustomerId(customerId))
                .FirstOrDefault();

            //assert
            Assert.False(result.HasError);
            Assert.Null(reservation);

        }

        [Fact]
        public void GivenBookingRoom_WhenCancelBookingNotExist_ThenReturnBookingError()
        {
            //arrange           
            BookingService bookingService = BuildBookingService();
            Guid customerId = Guid.NewGuid();            

            //act
            Result result = bookingService.CancelReservation(customerId);

            //assert
            Assert.True(result.HasError);
            Assert.Equal(StringMessages.ReservationNotFound, result.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenModifyBookingToInvalidPeriod_ThenReturnBookingError()
        {
            //arrange           
            BookingService bookingService = BuildBookingService();
            Guid customerId = Guid.NewGuid();
            var startDate = new DateTime(2023, 03, 7);
            var endDate = startDate.AddDays(3).AddMilliseconds(-1);
            bookingService.DoReservation(customerId, _roomId, startDate, endDate);

            var newStartDate = startDate.AddDays(40);
            var newEndDate = endDate.AddDays(40);

            var newReservation = _reservationRepositoryMock
                .GetAll(BookingQuery.GetReservationByCustomerId(customerId))
                .FirstOrDefault();

            //act
            var exception = Assert.Throws<BookingException>(() =>
            {
                Result result = bookingService
                                .ModifyReservation(newReservation.Id, customerId, _roomId, newStartDate, newEndDate);

            });            

            //assert            
            Assert.Equal(StringMessages.Reservation30DaysErrorMessage, exception.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenModifyInexistingBooking_ThenReturnBookingError()
        {
            //arrange           
            BookingService bookingService = BuildBookingService();
            Guid customerId = Guid.NewGuid();
            var startDate = new DateTime(2023, 03, 15);
            var endDate = startDate.AddDays(3).AddMilliseconds(-1);                                

            //act
            Result result = bookingService
                .ModifyReservation(Guid.NewGuid(), customerId, _roomId, startDate, endDate);

            //assert
            Assert.True(result.HasError);
            Assert.Equal(StringMessages.ReservationNotFound, result.Message);
        }

        [Fact]
        public void GivenBookingRoom_WhenModifyToValidPeriod_ThenReturnBookingModified()
        {
            //arrange           
            BookingService bookingService = BuildBookingService();
            Guid customerId = Guid.NewGuid();
            var startDate = new DateTime(2023, 03, 7);
            var endDate = startDate.AddDays(3).AddMilliseconds(-1);
            bookingService.DoReservation(customerId, _roomId, startDate, endDate);

            var newStartDate = startDate.AddDays(10);
            var newEndDate = endDate.AddDays(10);

            var newReservation = _reservationRepositoryMock
                .GetAll(BookingQuery.GetReservationByCustomerId(customerId))
                .FirstOrDefault()!;

            //act
            Result result = bookingService
                .ModifyReservation(newReservation.Id, customerId, _roomId, newStartDate, newEndDate);

            var newReservationUpdated = _reservationRepositoryMock
                .GetAll(BookingQuery.GetReservationByCustomerId(customerId))
                .FirstOrDefault()!;

            //assert
            Assert.False(result.HasError);
            Assert.Equal(newReservationUpdated.StartDate, newStartDate);
            Assert.Equal(newReservationUpdated.EndDate, newEndDate);
            Assert.Equal(newReservationUpdated.CustomerId, customerId);
            Assert.Equal(newReservationUpdated.RoomId, _roomId);
        }

        BookingService BuildBookingService()
        {
            CurrentDateTimeMock currentDateTime 
                = new CurrentDateTimeMock(new DateTime(2023, 02, 23, 6, 30, 30));
            
            return
                new BookingService(_reservationRepositoryMock                
                , currentDateTime);
        }
        void SetupReservation()
        {
            var StartDate = new DateTime(2023, 02, 24);
            var EndDate = new DateTime(2023, 02, 26, 23, 59, 59);
            Reservation reservation1 = new Reservation
            {
                CustomerId = _customerId,
                RoomId = _roomId,
                StartDate = StartDate,
                EndDate = EndDate
            };

            _reservationRepositoryMock = new RepositoryMock<Reservation>();
            _reservationRepositoryMock.Add(reservation1);
        }
        void SetupRoom()
        {
            Room room = new Room
            {
                Id = _roomId,
                Type = RoomType.DOUBLE
            };
            _roomRepositoryMock = new RepositoryMock<Room>();
            _roomRepositoryMock.Add(room);
        }
        void SetupCustomer()
        {
            Customer customer = new Customer
            {
                Id = _customerId
                ,
                DateOfBirth = new DateOnly(1990, 01, 01)
                ,
                Document = "00-0001"
                ,
                Email = "test@test.com"
                ,
                FirstName = "Felipe"
                ,
                LastName = "Gonçalves"
                ,
                Genre = Genre.MALE
            };

            _customerRepositoryMock = new RepositoryMock<Customer>();
            _customerRepositoryMock.Add(customer);
        }
    }
}
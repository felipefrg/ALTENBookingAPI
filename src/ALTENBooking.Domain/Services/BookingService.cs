using ALTENBooking.Domain.Interfaces;
using ALTENBooking.Domain.Models;
using ALTENBooking.Domain.Queries;
using ALTENBooking.Utils;

namespace ALTENBooking.Domain.Services
{
    public class BookingService : IBookingService
    {
        IRepository<Reservation> _reservationRepository;
        IRepository<Room> _roomRepository;
        DateTime _currentDateTime;

        public BookingService(IRepository<Reservation> reservationRepository
                             , IRepository<Room> roomRepository
                              , ICurrentDateTime currentDateTime)
        {   
            _reservationRepository = reservationRepository;
            _currentDateTime = currentDateTime.Value;
            _roomRepository = roomRepository;
        }
        public Result CancelReservation(Guid customerId)
        {
            var result = new Result();
            var reservation = _reservationRepository
                .GetAll(BookingQuery.GetReservationByCustomerId(customerId))
                .FirstOrDefault();

            if (reservation == null)
            {
                result.HasError = true;
                result.Message = StringMessages.ReservationNotFound;
                return result;
            }            
            _reservationRepository.Delete(reservation);
            result.Message = StringMessages.ReservationDeleteOK;
            return result;
        }

        public Result DoReservation(Guid customerId, Guid roomId, DateTime startDate, DateTime endDate)
        {
            CheckEndDateMinorThanStartDate(startDate);
            CheckReservationGreatThan30Days(startDate);
            CheckEndDateGreatThanEndDate(startDate, endDate);
            CheckReservationStayGreatThan3Days(startDate, endDate);                  
            CheckRoomIsAvailable(startDate, endDate, roomId);
            CheckUserAlreadyBooking(customerId, roomId);

            var reservation = new Reservation { 
                CustomerId = customerId
                ,RoomId = roomId
                ,StartDate = startDate
                ,EndDate = endDate                
            };
            _reservationRepository.Add(reservation);

            Result result = new Result();
            result.Message = StringMessages.ReservationCreateOK;

            return result; 
        }

        public bool HasAnyRoomAvailable(DateTime startDate, DateTime endDate)
        {
            var result = _reservationRepository
                .GetAll(BookingQuery.RoomAvailability(startDate,endDate))
                .FirstOrDefault();               

            return result == null;
        }

        public Result ModifyReservation(Guid reservationId, Guid customerId, Guid roomId, DateTime startDate, DateTime endDate)
        {
            var result = new Result();
            var reservation = _reservationRepository.GetById(reservationId);

            if (reservation == null)
            {
                result.HasError = true;
                result.Message = StringMessages.ReservationNotFound;
                return result;
            }

            CheckEndDateMinorThanStartDate(startDate);
            CheckReservationGreatThan30Days(startDate);
            CheckEndDateGreatThanEndDate(startDate, endDate);
            CheckReservationStayGreatThan3Days(startDate, endDate);
            CheckRoomIsAvailable(startDate, endDate, roomId);            

            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.CustomerId = customerId;
            reservation.RoomId = roomId;

            _reservationRepository.Update(reservation);
            result.Message = StringMessages.ReservationModificateOK;
            return result;
        }

        public IList<Reservation> GetReservationByDate(DateTime startDate, DateTime endDate)
        {
            var rooms = _reservationRepository.GetAll(BookingQuery.GetRoomByDate(startDate, endDate)).ToList();
            return rooms;
        }

        void CheckEndDateMinorThanStartDate(DateTime startDate)
        {
            if (startDate < _currentDateTime)
            {
                throw new BookingException(StringMessages.ReservationStartDateMinorThanCurrentDate);
            }
        }

        void CheckReservationStayGreatThan3Days(DateTime startDate, DateTime endDate)
        {
            if (endDate.Subtract(startDate) > new TimeSpan(3, 0, 0, 0))
            {
                throw new BookingException(StringMessages.ReservationStayGreatThan3DaysInRowErrorMessage);
            }
        }

        void CheckEndDateGreatThanEndDate(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new BookingException(StringMessages.ReservationStartDateGreatThanEndDateErrorMessage);
            }
        }

        void CheckReservationGreatThan30Days(DateTime startDate)
        {
            var futureDateTime = _currentDateTime.AddDays(30);
            if (startDate > futureDateTime)
            {
                throw new BookingException(StringMessages.Reservation30DaysErrorMessage);
            }
        }

        void CheckUserAlreadyBooking(Guid customerId, Guid roomId)
        {
            var reservation = _reservationRepository
                .GetAll(BookingQuery.CustomerAlreadyHasValidReservation(customerId, roomId, _currentDateTime))
                .FirstOrDefault();

            if (reservation != null)
                throw new BookingException(StringMessages.ReservationUserAlreadyBookedRoom);
        }

        void CheckRoomIsAvailable(DateTime startDate, DateTime endDate, Guid roomId)
        {
            var room = _roomRepository
               .GetAll(BookingQuery.GetRoomById(roomId))
               .FirstOrDefault();

            if(room == null)
                throw new BookingException(StringMessages.RoomNotExists);

            var reservation = _reservationRepository
               .GetAll(BookingQuery.RoomAvailability(startDate, endDate, roomId))
               .FirstOrDefault();

            if (reservation != null)
                throw new BookingException(StringMessages.ReservationRoomUnavailable);
        }        

        public IList<Reservation> GetAllReservation()
        {
            return _reservationRepository.GetAll().ToList();
        }
    }
}

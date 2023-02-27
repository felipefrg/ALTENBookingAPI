using ALTENBooking.Application.DTOs;
using ALTENBooking.Application.Interfaces;
using ALTENBooking.Domain;
using ALTENBooking.Domain.Models;
using ALTENBooking.Utils;
using IBookingServiceDomain = ALTENBooking.Domain.Interfaces.IBookingService;
using ICustomerServiceDomain = ALTENBooking.Domain.Interfaces.ICustomerService;

namespace ALTENBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        IBookingServiceDomain _bookingServiceDomain;
        ICustomerServiceDomain _customerServiceDomain;

        public BookingService(IBookingServiceDomain bookingServiceDomain, ICustomerServiceDomain customerServiceDomain)
        {
            _bookingServiceDomain = bookingServiceDomain;
            _customerServiceDomain = customerServiceDomain;
        }

        public ResultDTO CancelReservation(Guid customerId)
        {
            return Utils.Execute(() => { 
                ResultDTO result = new ResultDTO();

                var domainResult = _bookingServiceDomain.CancelReservation(customerId);                
                result = ResultDTO.FromModel(domainResult);

                return result;
            
            });
        }       

        public ResultDTO DoReservation(BookingDTO createBooking)
        {
            return Utils.Execute(() => {
                
                Customer customer;

                //CheckUser Exists
                customer = _customerServiceDomain.GetByEmail(createBooking?.Customer?.Email ?? String.Empty);
                if (customer == null)
                {
                    //Create User
                    customer = createBooking.Customer.ToModel();
                    Result domainResultCreate = _customerServiceDomain.Create(customer);

                    if(domainResultCreate.HasError)                    
                        return ResultDTO.FromModel(domainResultCreate);
                }

                var date = new StartDateEndDate(createBooking.StartDate, createBooking.EndDate);               

                var reservationResult = _bookingServiceDomain.DoReservation(customer.Id, createBooking.RoomId, date.StartDate, date.EndDate);
                var result = ResultDTO.FromModel(reservationResult);                                

                return result;

            });
        }

        public IList<BookingDTO> GetReservation()
        {
            List<BookingDTO> reservations = new List<BookingDTO>();

            var reservation = _bookingServiceDomain.GetAllReservation();
            foreach(var reservationItem in reservation)
            {                
                var customer = _customerServiceDomain.Get(reservationItem.CustomerId);
                BookingDTO bookingDTO = BookingDTO.FromModel(reservationItem, customer);
                reservations.Add(bookingDTO);
            }

            return reservations;
        }

        public bool IsRoomAvailable(DateTime startDate, DateTime endDate)
        {
            StartDateEndDate dateTime = new StartDateEndDate(startDate, endDate);
            return _bookingServiceDomain.HasAnyRoomAvailable(dateTime.StartDate, dateTime.EndDate);
        }

        public ResultDTO ModifyReservation(BookingUpdateDTO updateBooking)
        {
            return Utils.Execute(() => {

                ResultDTO result = new ResultDTO();
                if (updateBooking.Id == Guid.Empty)
                {
                    result.HasError = true;
                    result.Message = StringMessages.BookingIdNotNull;
                    return result;
                }

                if (!(updateBooking.CustomerId == Guid.Empty))
                {
                    result.HasError = true;
                    result.Message = StringMessages.CustomerIdNotNull;
                    return result;
                }

                var date = new StartDateEndDate(updateBooking.StartDate, updateBooking.EndDate);

                var domainResult = _bookingServiceDomain.ModifyReservation(updateBooking.Id, updateBooking.CustomerId, updateBooking.RoomId, updateBooking.StartDate, updateBooking.EndDate);
                return ResultDTO.FromModel(domainResult);
            });
        }
    }
}

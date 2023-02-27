using ALTENBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Application.DTOs
{
    public class BookingDTO
    {        
        public Guid? Id { get; set; }
        public CustomerDTO Customer { get; set; }
        public Guid RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }    
        
        public static BookingDTO FromModel(Reservation reservation, Customer customer)
        {
            return new BookingDTO {
                Id = reservation.Id,
                RoomId = reservation.RoomId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Customer = CustomerDTO.FromModel(customer)
            };
        }
    }
}

using ALTENBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALTENBooking.Domain.Models
{
    public class Room : BaseEntity
    {        
        public RoomType Type { get; set; }
    }
}

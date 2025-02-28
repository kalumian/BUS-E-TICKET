using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class CreateReservationDTO
    {
        public int? ReservationID { get; set; }
        public int TripID { get; set; }
        public int? CustomerID { get; set; }
        public PassengerDTO? Passenger { get; set; }
        public EnPaymentMethod PaymentMethod { get; set; }
    }

}

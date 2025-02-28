using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class BookingDTO
    {
        public int? BookingID { get; set; }
        public int? TripID { get; set; }
        public string BookingStatus { get; set; }
        public DateTime BookingDate { get; set; }
        public string PassengerName { get; set; }
        public string PassengerNationalID { get; set; }
        public string pnr { get; set; }
        public DateTime TripDate { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
    }
}

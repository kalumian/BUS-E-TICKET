using Core_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class TicketDTO
    {
        public int TicketID { get; set; }
        public string PNR { get; set; }
        public DateTime IssueDate { get; set; }

        // Invoice Details
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceIssueDate { get; set; }

        // Payment Details
        public string PaymentMethod { get; set; }
        public string OrderID { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsRefundable { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TripAmount { get; set; }
        public string VAT { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountAmount { get; set; }

        // Reservation Details
        public DateTime ReservationDate { get; set; }

        // Trip Details
        public string VehicleInfo { get; set; }
        public string DriverInfo { get; set; }
        public DateTime TripDate { get; set; }
        public string Currency { get; set; }
        public TimeSpan TripDuration { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }

        // Service Provider Details
        public string BusinessName { get; set; }
        public string LogoURL { get; set; }

        // Passenger Details
        public string PassengerName { get; set; }
        public string PassengerNationalID { get; set; }
        public string PassengerGender { get; set; }
    }
}

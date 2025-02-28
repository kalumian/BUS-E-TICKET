using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class InvoiceDTO
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderID { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsRefundable { get; set; }
        public DateTime PaymentDate { get; set; }
        public string VAT { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string TripAmount { get; set; }
        public string Currency { get; set; }
        public string BusinessName { get; set; }
        public string LogoURL { get; set; }
    }

}

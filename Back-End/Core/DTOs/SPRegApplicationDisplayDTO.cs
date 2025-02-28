using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class SPRegApplicationDisplayDTO
    {
        public string AccountID { get; set; }
        public int SPRegRequestID { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Notes { get; set; }
        public string? BusinessName { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessPhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? ServiceProviderEmail { get; set; }
        public string? ServiceProviderPhoneNumber { get; set; }
        public EnRegisterationRequestStatus? ApplicationStatus { get; set; }
        public SPRegResponseDTO? Review {  get; set; }
    }
}

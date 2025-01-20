using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class BusinessDTO
    {
        public int BusinessID { get; set; }
        public string? BusinessName { get; set; }
        public string? LogoURL { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebSiteLink { get; set; }
        public string? BusinessLicenseNumber { get; set; }
        public AddressDTO? Address { get; set; }
        public ContactInformationDTO? ContactInformation { get; set; }

    }
}

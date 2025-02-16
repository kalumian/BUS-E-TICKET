using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ServiceProviderDTO
    {
        // مزود الخدمة
        public int ServiceProviderID { get; set; }
        public string? AccountID { get; set; }

        // بيانات الحساب
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime RegisterationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public EnAccountStatus AccountStatus { get; set; }

        // بيانات البزنس
        public int BusinessID { get; set; }
        public string? BusinessName { get; set; }
        public string? LogoURL { get; set; }
        public string? BusinessLicenseNumber { get; set; }
        public string BussinesEmail { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string? WebSiteLink { get; set; }

        // بيانات العنوان
        public int AddressID { get; set; }
        public string? AdditionalDetails { get; set; }
        public string? StreetName { get; set; }
        public string? CityName { get; set; }
        public string? RegionName { get; set; }
        public string? CountryName { get; set; }
        public string? CountryID { get; set; }
        public string? RegionID { get; set; }
        public string? CityID { get; set; }


        // بيانات الاتصال
        public int ContactInformationID { get; set; }
        public string? MobileNumber2 { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? LandLineNumber { get; set; }
    }
}

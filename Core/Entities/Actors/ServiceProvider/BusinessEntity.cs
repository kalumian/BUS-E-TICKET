using Core_Layer.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors.ServiceProvider
{
    public class BusinessEntity
    {
        [Key]
        public int BusinessID { get; set; }

        [Required(ErrorMessage = "BusinessName is required.")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "Business Name must be between 15 and 100 digits.")]
        public required string BusinessName { get; set; }

        [Url]
        [Length(0, 800, ErrorMessage = "Logo link must be lower then 800 chrachtaers")]
        public string? LogoURL { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Phone number must be between 9 and 15 digits.")]
        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Phone number must contain only digits and can start with a '+' sign.")]
        public required string PhoneNumber { get; set; }

        [Length(0, 800, ErrorMessage = "Website link must be lower then 800 chrachtaers")]
        [Url]
        public string? WebSiteLink { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = "BusinessLicenseNumber must be between 5 and 20 characters")]
        public string? BusinessLicenseNumber {  get; set; }


        // Foregin Keys
        [Required(ErrorMessage = "AddressID is required")]
        [ForeignKey("Address")]
        public required int AddressID { get; set; }

        [Required(ErrorMessage = "ContactInformationID is required")]
        [ForeignKey("ContactInformation")]
        public int ContactInformationID { get; set; }

        // Navigation Properties
        public AddressEntity? Address { get; set; }
        public ContactInformationEntity? ContactInformation { get; set; }

    }
}

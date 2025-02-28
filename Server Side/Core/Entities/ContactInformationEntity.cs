using Core_Layer.Entities.Actors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities
{
    public class ContactInformationEntity
    {
        [Key]
        public int? ContactInformationID { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Phone number must be between 9 and 15 digits.")]
        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Phone number must contain only digits and can start with a '+' sign.")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [StringLength(50, ErrorMessage = "Instagram handle cannot exceed 50 characters.")]
        public string? Instagram { get; set; }

        [StringLength(50, ErrorMessage = "Twitter handle cannot exceed 50 characters.")]
        public string? Twitter { get; set; }

        [StringLength(50, ErrorMessage = "Facebook handle cannot exceed 50 characters.")]
        public string? Facebook { get; set; }

        [StringLength(50, ErrorMessage = "LinkedIn handle cannot exceed 50 characters.")]
        public string? LinkedIn { get; set; }

        [StringLength(15, ErrorMessage = "Landline Number cannot exceed 15 characters.")]
        public string? LandLineNumber { get; set; }

        //
        public PersonEntity? Person { get; set; }

    }
}

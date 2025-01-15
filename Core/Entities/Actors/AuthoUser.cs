using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Enums;
using Core_Layer.Interfaces.Actors_Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors
{
    public class AuthoUser : IdentityUser
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Last Login Date")]
        public DateTime? LastLoginDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Registration Date")]
        [Required(ErrorMessage = "Registration Date is required.")]
        public DateTime RegisterationDate { get; set; }

        [Required(ErrorMessage = "Account status is required.")]
        [Display(Name = "Account Status")]
        public EnAccountStatus AccountStatus { get; set; }

        [Required(ErrorMessage = "Permission is required.")]
        public int Permission { get; set; } = 1; // Default permission

        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "UserName must be between 5 and 20 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "UserName can only contain letters, numbers, and underscores")]
        public override string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public override string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Phone number must be between 9 and 15 digits.")]
        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "Phone number must contain only digits and can start with a '+' sign.")]
        public override string? PhoneNumber { get; set; }

        // Navigation properties for related entities
        public IEnumerable<CustomerEntity>? Customers { get; set; }
        public IEnumerable<ServiceProviderEntity>? ServiceProviders { get; set; }
        public IEnumerable<ManagerEntity>? Managers { get; set; }
    }
}

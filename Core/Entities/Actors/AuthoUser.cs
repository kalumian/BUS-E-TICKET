using Core_Layer.Enums;
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

        // Navigation properties for related entities
        public IEnumerable<CustomerEntity>? Customers { get; set; }
        public IEnumerable<ServiceProviderEntity>? ServiceProviders { get; set; }
        public IEnumerable<ManagerEntity>? Managers { get; set; }
    }
}

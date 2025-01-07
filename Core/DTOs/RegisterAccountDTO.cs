using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class RegisterAccountDTO
    {
        [Required(ErrorMessage = "UserName Is Required")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public required string PhoneNumber { get; set; }
    }
}

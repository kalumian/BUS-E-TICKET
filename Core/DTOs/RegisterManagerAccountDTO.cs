using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class RegisterManagerAccountDTO : RegisterAccountDTO
    {
        [Required(ErrorMessage = "CreatedByID Is Required")]
        public required int CreatedByID { get; set; }
    }
}

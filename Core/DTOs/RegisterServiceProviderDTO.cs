using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class RegisterServiceProviderDTO
    {
        public int ServiceProviderID { get; set; }
        public int? BusinessID { get; set; }
        public required RegisterAccountDTO Account { get; set; }
    }
}

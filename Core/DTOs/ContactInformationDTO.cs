using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ContactInformationDTO
    {

        public int ContactInformationID { get; set; }
        public string? MobileNumber2 { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? LandLineNumber { get; set; }
    }
}

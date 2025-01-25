using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class AddressDTO
    {
        public int? AddressID { get; set; }
        public string? AdditionalDetails { get; set; }
        public int? StreetID { get; set; } 
        public int? CityID { get; set; }

    }
}

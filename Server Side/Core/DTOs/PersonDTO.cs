using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class PersonDTO
    {
        public int? PersonID { get; set; }
        public string? NationalID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public EnGender? Gender { get; set; }
        public ContactInformationDTO? ContactInformation {get; set;}

    }
}

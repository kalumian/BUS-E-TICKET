using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class PassengerDTO
    {
        public int? PassengerID { get; set; }
        public PersonDTO Person { get; set; } = new PersonDTO();
    }

}

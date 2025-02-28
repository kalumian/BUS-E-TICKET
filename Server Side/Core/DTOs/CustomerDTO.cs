using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class CustomerDTO
    {
        public required AccountDTO Account { get; set; }
        public required PersonDTO Person { get; set; }
        public required AddressDTO Address { get; set; }
        public required ContactInformationDTO ContactInformation { get; set; }
    }
}

using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ManagerDTO
    {
        public int ManagerID { get; set; }
        public string CreatedBy { get; set; }
        public string AccountID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public EnAccountStatus AccountStatus { get; set; }
    }
}

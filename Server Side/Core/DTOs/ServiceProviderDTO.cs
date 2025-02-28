using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ServiceProviderDTO
    {
        // مزود الخدمة
        public int ServiceProviderID { get; set; }
        public BusinessDTO Business { get; set; }
        public AccountDTO Account { get; set; }

    }
}

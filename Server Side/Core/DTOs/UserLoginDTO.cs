using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class UserLoginDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}

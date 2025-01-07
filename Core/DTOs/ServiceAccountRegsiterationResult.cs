using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ServiceAccountRegistrationResult
    {
        public bool IsSuccess { get; set; }
        public string? UserId { get; set; }
        public List<string>? Errors { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ApiResponse
    {
        public required bool IsSuccess { get; set; }
        public required string Message { get; set; }
        public object? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}

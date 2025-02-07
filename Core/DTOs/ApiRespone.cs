using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class ApiResponse
    {
        public string? Message { get; set; }
        public object? Data { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }
    }

}

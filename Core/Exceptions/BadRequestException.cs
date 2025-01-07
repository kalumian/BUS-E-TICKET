using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message) : base(400, message)
        {
        }
    }
}

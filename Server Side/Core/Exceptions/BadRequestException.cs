using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Exceptions
{
    public class BadRequestException(string message) : HttpException(400, message)
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Exceptions
{
    public class NotFoundException(string message) : HttpException(500, message)
    {
    }
}

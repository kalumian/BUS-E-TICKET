using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Exceptions
{
    internal class NotFoundException : HttpException
    {
        public NotFoundException(string message) : base(500, message)
        {
        }
    }
}

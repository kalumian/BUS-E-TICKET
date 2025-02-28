using Data_Access_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    abstract public class ServiceWithMapper(IUnitOfWork unitOfWork)
    {

        public IUnitOfWork _unitOfWork = unitOfWork;
    }
}

using Data_Access_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Actors.ServiceProvider
{
    public class SPRegResponseService : GeneralService
    {
        public SPRegResponseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

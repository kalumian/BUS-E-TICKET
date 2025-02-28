using Core_Layer.Entities;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public  class CurrencyService(IUnitOfWork unitOfWork) : GeneralService(unitOfWork)
    {
        public IEnumerable<CurrencyEntity> GetCurrencies()
        {
            return _unitOfWork.GetDynamicRepository<CurrencyEntity>().GetAll();
        }

    }
}

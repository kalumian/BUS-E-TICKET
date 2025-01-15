using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Locations;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class AddressService : GeneralService
    {
        public AddressService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<int> CreateEntity(AddressEntity Address)
        {
            CheckEntityExist<CityEntity>(i=>i.CityID == Address.CityID);
            await CreateEntity<AddressEntity>(Address);
            return Address.AddressID;
        }
    }
}

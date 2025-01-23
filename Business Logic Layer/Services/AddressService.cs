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
        public async Task<int> CreateEntityAsync(AddressEntity Address, bool saveChanges = false)
        {
            
            CheckEntityExist<CityEntity>(i=>i.CityID == Address.CityID);
            await CreateEntityAsync<AddressEntity>(Address, saveChanges);
            return Address.AddressID;
        }
    }
}

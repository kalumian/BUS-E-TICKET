using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities.Locations;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class AddressService(IUnitOfWork unitOfWork) : GeneralService(unitOfWork)
    {
        public async Task<int> CreateEntityAsync(AddressEntity Address, bool saveChanges = false)
        {
            
            CheckEntityExist<CityEntity>(i=>i.CityID == Address.CityID);
            await CreateEntityAsync<AddressEntity>(Address, saveChanges);
            return Address.AddressID;
        }

           
            public IEnumerable<CountryEntity> GetCountries()
            {
                return _unitOfWork.GetDynamicRepository<CountryEntity>().GetAll();
            }

            public IEnumerable<RegionEntity> GetRegionsByCountry(int countryID)
            {
                return [.. _unitOfWork.GetDynamicRepository<RegionEntity>().GetAllQueryable().Where(r => r.CountryID == countryID)];
            }

            public IEnumerable<CityEntity> GetCitiesByRegion(int regionID)
            {
                return [.. _unitOfWork.GetDynamicRepository<CityEntity>().GetAllQueryable().Where(c => c.RegionID == regionID)];
            }
             public IEnumerable<CityEntity> GetAllCities()
            {
                return [.. _unitOfWork.GetDynamicRepository<CityEntity>().GetAllQueryable()];
            }
        public async Task Init()
        {
        //    var x = new List<RegionEntity> {

        //    new RegionEntity {CountryID=2, RegionName = "Riyadh" },
        //    new RegionEntity {CountryID=2, RegionName = "Makkah" },
        //    new RegionEntity {CountryID=2, RegionName = "Medina" },
        //    new RegionEntity {CountryID=2, RegionName = "Qassim" },
        //    new RegionEntity {CountryID=2, RegionName = "Eastern Province" },
        //    new RegionEntity {CountryID=2, RegionName = "Asir" },
        //    new RegionEntity {CountryID=2, RegionName = "Jizan" },
        //    new RegionEntity {CountryID=2, RegionName = "Najran" },
        //    new RegionEntity {CountryID=2, RegionName = "Ha'il" },
        //    new RegionEntity {CountryID=2, RegionName = "Northern Borders" },
        //    new RegionEntity {CountryID=2, RegionName = "Al-Bahah" },
        //    new RegionEntity {CountryID=2, RegionName = "Tabuk" },
        //    new RegionEntity {CountryID=2, RegionName = "Al-Jouf" },
        //    new RegionEntity {CountryID=2, RegionName = "Al-Qassim" }
        //};
        //    _unitOfWork.GetDynamicRepository<RegionEntity>().AddRange(x);
        }
    }
}

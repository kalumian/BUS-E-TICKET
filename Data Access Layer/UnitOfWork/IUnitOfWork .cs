using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Locations;
using Data_Access_Layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<CustomerEntity> Customers { get; }
        IRepository<PersonEntity> People { get; }
        IRepository<ServiceProviderEntity> ServiceProviders { get; }
        IRepository<ManagerEntity> Managers { get; }
        IRepository<AddressEntity> Addresses { get; }
        IRepository<ContactInformationEntity> ContactInformations { get; }
        public IRepository<T> GetDynamicRepository<T>() where T : class;

        Task SaveChangesAsync();
        void SaveChanges();


    }
}

using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
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
        IUserRepository<CustomerEntity> Customers { get; }
        IRepository<PersonEntity> People { get; }
        IUserRepository<ServiceProviderEntity> ServiceProviders { get; }
        IUserRepository<ManagerEntity> Managers { get; }
        IRepository<AddressEntity> Addresses { get; }
        IRepository<BusinessEntity> Businesses { get; }
        IRepository<SPRegRequestEntity> SPRegRequests { get; }
        IRepository<SPRegRequestEntity> SPRegResponses { get; }
        IRepository<ContactInformationEntity> ContactInformations { get; }
        public IRepository<T> GetDynamicRepository<T>() where T : class;
        void Attach<T>(T entity) where T : class;
        Task SaveChangesAsync();
        void SaveChanges();


    }
}

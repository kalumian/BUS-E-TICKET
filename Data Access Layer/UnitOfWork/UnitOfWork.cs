using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Data_Access_Layer.Data;
using Data_Access_Layer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository<CustomerEntity> Customers { get; private set; }
        public IUserRepository<ManagerEntity> Managers { get; private set; }
        public IUserRepository<ServiceProviderEntity> ServiceProviders { get; private set; }
        public IRepository <PersonEntity> People { get; private set; }
        public IRepository<AddressEntity> Addresses { get; private set; }
        public IRepository<ContactInformationEntity> ContactInformations { get; private set; }

        public IRepository<BusinessEntity> Businesses { get; private set; }

        public IRepository<SPRegRequestEntity> SPRegRequests { get; private set; }

        public IRepository<SPRegRequestEntity> SPRegResponses { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            _context = dbContext;
            Customers = new UserRepository<CustomerEntity>(dbContext);
            People = new GeneralRepository<PersonEntity>(dbContext);
            ServiceProviders = new UserRepository<ServiceProviderEntity>(dbContext);
            Managers = new UserRepository<ManagerEntity>(dbContext);
            SPRegRequests = new UserRepository<SPRegRequestEntity>(dbContext);
            Businesses = new UserRepository<BusinessEntity>(dbContext);
            Addresses = new GeneralRepository<AddressEntity>(dbContext);
            ContactInformations = new GeneralRepository<ContactInformationEntity>(dbContext);
            SPRegResponses = new GeneralRepository<SPRegRequestEntity>(dbContext);

        }
        public IRepository<T> GetDynamicRepository<T>() where T : class
        {
            return new GeneralRepository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void  SaveChanges()
        {
             _context.SaveChanges();
        }
        public void Attach<T>(T entity) where T : class
        {
            _context.Attach(entity);
        }
    }
}

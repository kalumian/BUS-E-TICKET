using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Core_Layer.Entities.Payment;
using Core_Layer.Entities.Trip;
using Core_Layer.Entities.Trip.Reservation;
using Data_Access_Layer.Data;
using Data_Access_Layer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.UnitOfWork
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _context = dbContext;
        public IRepository<CustomerEntity> Customers { get; private set; } = new GeneralRepository<CustomerEntity>(dbContext);
        public IRepository<LocationEntity> Locations { get; } = new GeneralRepository<LocationEntity>(dbContext);
        public IRepository<ManagerEntity> Managers { get; private set; } = new GeneralRepository<ManagerEntity>(dbContext);
        public IRepository<ServiceProviderEntity> ServiceProviders { get; private set; } = new GeneralRepository<ServiceProviderEntity>(dbContext);
        public IRepository<PersonEntity> People { get; private set; } = new GeneralRepository<PersonEntity>(dbContext);
        public IRepository<AddressEntity> Addresses { get; private set; } = new GeneralRepository<AddressEntity>(dbContext);
        public IRepository<TripEntity> Trips { get; private set; } = new GeneralRepository<TripEntity>(dbContext);
        public IRepository<ContactInformationEntity> ContactInformations { get; private set; } = new GeneralRepository<ContactInformationEntity>(dbContext);
        public IRepository<BusinessEntity> Businesses { get; private set; } = new GeneralRepository<BusinessEntity>(dbContext);
        public IRepository<PassengerEntity> Passengers { get; private set; } = new GeneralRepository<PassengerEntity>(dbContext);
        public IRepository<SPRegRequestEntity> SPRegRequests { get; private set; } = new GeneralRepository<SPRegRequestEntity>(dbContext);
        public IRepository<SPRegResponseEntity> SPRegResponses { get; private set; } = new GeneralRepository<SPRegResponseEntity>(dbContext);
        public IRepository<PaymentEntity> Payments { get; private set; } = new GeneralRepository<PaymentEntity>(dbContext);
        public IRepository<InvoiceEntity> Invoices { get; private set; } = new GeneralRepository<InvoiceEntity>(dbContext);
        public IRepository<ReservationEntity> Reservations { get; private set; } = new GeneralRepository<ReservationEntity>(dbContext);
        public IRepository<TicketEntity> Tickets { get; } = new GeneralRepository<TicketEntity>(dbContext);

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
        public void AttachEntity<T>(T entity) where T : class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
        }
        public EntityEntry<T> Entry<T>(T entity) where T : class
        {
            return _context.Entry(entity);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

    }
}

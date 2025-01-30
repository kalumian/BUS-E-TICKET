using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Core_Layer.Entities.Payment;
using Core_Layer.Entities.Trip;
using Core_Layer.Entities.Trip.Reservation;
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
    public interface IUnitOfWork
    {
        IUserRepository<CustomerEntity> Customers { get; }
        IRepository<PersonEntity> People { get; }
        IUserRepository<ServiceProviderEntity> ServiceProviders { get; }
        IUserRepository<ManagerEntity> Managers { get; }
        IRepository<AddressEntity> Addresses { get; }
        IRepository<LocationEntity> Locations { get; }
        IRepository<BusinessEntity> Businesses { get; }
        IRepository<SPRegRequestEntity> SPRegRequests { get; }
        IRepository<SPRegResponseEntity> SPRegResponses { get; }
        IRepository<TripEntity> Trips { get; }
        IRepository<PaymentEntity> Payments { get; }
        IRepository<PassengerEntity> Passengers { get; }
        IRepository<ReservationEntity> Reservations { get; }
        IRepository<ContactInformationEntity> ContactInformations { get; }
        public IRepository<T> GetDynamicRepository<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        Task SaveChangesAsync();
        void SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        public void AttachEntity<T>(T entity) where T : class;
    }
}

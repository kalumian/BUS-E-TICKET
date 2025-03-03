﻿using Microsoft.EntityFrameworkCore;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Trip;
using Core_Layer.Entities.Locations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core_Layer.Entities.Payment;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities;
using Core_Layer.Entities.Trip.Reservation;

namespace Data_Access_Layer.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> option) : IdentityDbContext<AuthoUser>(option) {
        public DbSet<PersonEntity> People { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<StreetEntity> Streets { get; set; }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<ServiceProviderEntity> ServiceProviders { get; set; }
        public DbSet<ManagerEntity> Managers { get; set; }
        public DbSet<SPRegRequestEntity> SPRegRequests { get; set; }
        public DbSet<SPRegResponseEntity> SPRegResponses { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<PassengerEntity> Passengers { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<CurrencyEntity> Currencies { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<BusinessEntity> Businesses { get; set; }
        public DbSet<ContactInformationEntity> ContactInformations {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessEntity>()
            .HasOne(b => b.ServiceProvider)
            .WithOne(sp => sp.Business)
            .HasForeignKey<ServiceProviderEntity>(sp => sp.BusinessID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonEntity>()
            .HasIndex(p => p.NationalID)
            .IsUnique()
            .HasDatabaseName("IX_Person_NationalID");
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var foreignKey in entityType.GetForeignKeys()) 
                        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);

        }

    }
}

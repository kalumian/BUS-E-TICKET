﻿using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Core_Layer.Entities.Payment;
using Core_Layer.Entities.Trip;
using Core_Layer.Entities.Trip.Reservation;
using Core_Layer.Enums;

namespace Core_Layer.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // General Mappings
            CreateMap<AuthoUser, RegisterAccountDTO>().ReverseMap();
            CreateMap<RegisterManagerAccountDTO, RegisterAccountDTO>();

            // Person and Address Mappings
            CreateMap<PersonEntity, PersonDTO>().ReverseMap();
            CreateMap<ContactInformationEntity, ContactInformationDTO>().ReverseMap();
            CreateMap<AddressEntity, AddressDTO>().ReverseMap();

            // Customer Mappings
            CreateMap<RegisterCustomerAccountDTO, CustomerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.Address.AddressID))
                .ForMember(dest => dest.PersonID, opt => opt.MapFrom(src => src.Person.PersonID))
                .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformation.ContactInformationID))
                .ReverseMap();

            // Manager Mappings
            CreateMap<RegisterManagerAccountDTO, ManagerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.CreatedByID, opt => opt.MapFrom(src => src.CreatedByID))
                .ReverseMap();

            // Service Provider Mappings
            CreateMap<RegisterServiceProviderDTO, ServiceProviderEntity>()
                .ForMember(dest => dest.ServiceProviderID, opt => opt.MapFrom(src => src.ServiceProviderID))
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.BusinessID))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                .ReverseMap();

            // Business Mappings
            CreateMap<BusinessDTO, BusinessEntity>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.Address != null ? src.Address.AddressID : null))
                .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation))
                .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformation != null ? src.ContactInformation.ContactInformationID : null))
                .ReverseMap();

            // Service Provider Registration Requests
            CreateMap<SPRegRequestDTO, SPRegRequestEntity>()
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business))
                .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.Business.BusinessID));

            CreateMap<SPRegRequestEntity, SPRegRequestDTO>()
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business))
                .ForMember(dest => dest.ServiceProvider, opt => opt.MapFrom(src => src.Business.ServiceProvider))
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business));


            // Service Provider Registration Responses
            CreateMap<SPRegResponseDTO, SPRegResponseEntity>()
                .ForMember(dest => dest.ResponseID, opt => opt.MapFrom(src => src.ResponseID))
                .ForMember(dest => dest.ResponseText, opt => opt.MapFrom(src => src.ResponseText))
                .ForMember(dest => dest.ResponseDate, opt => opt.MapFrom(src => src.ResponseDate))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
                .ForMember(dest => dest.RequestID, opt => opt.MapFrom(src => src.RequestID))
                .ForMember(dest => dest.RespondedByID, opt => opt.MapFrom(src => src.RespondedByID))
                .ForMember(dest => dest.RespondedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Request, opt => opt.Ignore())
                .ReverseMap();

            // Map TripEntity to TripRegistrationDTO
            CreateMap<TripEntity, TripRegistrationDTO>()
                .ForMember(dest => dest.StartLocation, opt => opt.MapFrom(src => src.StartLocation))
                .ForMember(dest => dest.EndLocation, opt => opt.MapFrom(src => src.EndLocation));

            // Map TripRegistrationDTO to TripEntity
            CreateMap<TripRegistrationDTO, TripEntity>()
                .ForMember(dest => dest.StartLocation, opt => opt.Ignore()) // يتم تعيين المواقع يدويًا
                .ForMember(dest => dest.EndLocation, opt => opt.Ignore());

            // Map LocationEntity to LocationDTO
            CreateMap<LocationEntity, LocationDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Map LocationDTO to LocationEntity
            CreateMap<LocationDTO, LocationEntity>()
                .ForMember(dest => dest.Address, opt => opt.Ignore()); // يتم تعيين العنوان يدويًا

            CreateMap<TripEntity, TripDisplayDTO>()
            .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.ServiceProvider.Business.BusinessName ?? "Unknown"))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.CurrencyCode ?? "Unknown"))
            .ForMember(dest => dest.BusinessLogoURL, opt => opt.MapFrom(src => src.ServiceProvider.Business.LogoURL ?? string.Empty))
            .ForMember(dest => dest.StartLocationName, opt => opt.MapFrom(src => src.StartLocation.LocationName ?? "Unknown"))
            .ForMember(dest => dest.StartLocationURL, opt => opt.MapFrom(src => src.StartLocation.LocationURL ?? string.Empty))
            .ForMember(dest => dest.StartCity, opt => opt.MapFrom(src => src.StartLocation.Address.City.CityName ?? "Unknown"))
            .ForMember(dest => dest.StartAdditionalDetails, opt => opt.MapFrom(src => src.StartLocation.Address.AdditionalDetails ?? string.Empty))
            .ForMember(dest => dest.EndLocationName, opt => opt.MapFrom(src => src.EndLocation.LocationName ?? "Unknown"))
            .ForMember(dest => dest.EndLocationURL, opt => opt.MapFrom(src => src.EndLocation.LocationURL ?? string.Empty))
            .ForMember(dest => dest.EndCity, opt => opt.MapFrom(src => src.EndLocation.Address.City.CityName ?? "Unknown"))
            .ForMember(dest => dest.EndAdditionalDetails, opt => opt.MapFrom(src => src.EndLocation.Address.AdditionalDetails ?? string.Empty));

            // Reservation Mapping
            CreateMap<ReservationEntity, CreateReservationDTO>();
            CreateMap<CreateReservationDTO, ReservationEntity>()
                .ForMember(dest => dest.ReservationID, opt => opt.MapFrom(src => src.ReservationID))
                .ForMember(dest => dest.TripID, opt => opt.MapFrom(src => src.TripID))
                .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.PassengerID, opt => opt.MapFrom(src => src.Passenger.PassengerID)) // تحويل PassengerID من PassengerDTO
                .ForMember(dest => dest.ReservationStatus, opt => opt.MapFrom(src => EnReservationStatus.Pending)) // تعيين حالة الحجز بشكل افتراضي
                .ForMember(dest => dest.ReservationDate, opt => opt.MapFrom(src => DateTime.UtcNow)); // تعيين تاريخ الحجز الحالي
            // Person Mapping
            CreateMap<PersonEntity, PersonDTO>().ReverseMap();
        }
    }
}

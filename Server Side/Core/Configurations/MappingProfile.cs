using AutoMapper;
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
using Microsoft.Extensions.DependencyInjection;

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
                .ReverseMap();

            // Manager Mappings
            CreateMap<RegisterManagerAccountDTO, ManagerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.CreatedByID, opt => opt.MapFrom(src => Convert.ToUInt32(src.CreatedByID)))
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
                .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.Business.BusinessID))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => DateTime.UtcNow));


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
                .ForMember(dest => dest.StartLocation, opt => opt.Ignore())
                .ForMember(dest => dest.EndLocation, opt => opt.Ignore());

            // Map LocationEntity to LocationDTO
            CreateMap<LocationEntity, LocationDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Map LocationDTO to LocationEntity
            CreateMap<LocationDTO, LocationEntity>()
                .ForMember(dest => dest.Address, opt => opt.Ignore());

            CreateMap<TripEntity, TripDisplayDTO>()
            .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.ServiceProvider.Business.BusinessName ?? "Unknown"))
            .ForMember(dest => dest.TripDate, opt => opt.MapFrom(src => src.TripDate))
            .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.TripDate.Add(src.TripDuration)))
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
                .ForMember(dest => dest.PassengerID, opt => opt.MapFrom(src => src.Passenger.PassengerID))
                .ForMember(dest => dest.ReservationStatus, opt => opt.MapFrom(src => EnReservationStatus.Pending))
                .ForMember(dest => dest.ReservationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            // Person Mapping
            CreateMap<PersonEntity, PersonDTO>().ReverseMap();

            CreateMap<ManagerEntity, ManagerDTO>()
             .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.AccountID))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Account.UserName))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Account.PhoneNumber))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
             .ForMember(dest => dest.RegisterationDate, opt => opt.MapFrom(src => src.Account.RegisterationDate))
             .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.Account.LastLoginDate))
             .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Account.UserName))
             .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.Account.AccountStatus))
             .ReverseMap();

            CreateMap<ServiceProviderEntity, ServiceProviderDTO>()
                  .ForMember(dest => dest.ServiceProviderID, opt => opt.MapFrom(src => src.ServiceProviderID))
                  .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account)) // Account
                  .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business)); // Business

        // Mapping for BusinessEntity to BusinessDTO
        CreateMap<BusinessEntity, BusinessDTO>()
            .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.BusinessID))
            .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.BusinessName))
            .ForMember(dest => dest.LogoURL, opt => opt.MapFrom(src => src.LogoURL))
            .ForMember(dest => dest.WebSiteLink, opt => opt.MapFrom(src => src.WebSiteLink))
            .ForMember(dest => dest.BusinessLicenseNumber, opt => opt.MapFrom(src => src.BusinessLicenseNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)) // Address
            .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation));// ContactInformation

        // Mapping for ContactInformationEntity to ContactInformationDTO
        CreateMap<ContactInformationEntity, ContactInformationDTO>()
            .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformationID))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
            .ForMember(dest => dest.Twitter, opt => opt.MapFrom(src => src.Twitter))
            .ForMember(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
            .ForMember(dest => dest.LinkedIn, opt => opt.MapFrom(src => src.LinkedIn))
            .ForMember(dest => dest.LandLineNumber, opt => opt.MapFrom(src => src.LandLineNumber));

        // Mapping for AuthoUser to AccountDTO
        CreateMap<AuthoUser, AccountDTO>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id)) // IdentityUser's Id -> AccountId
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.RegisterationDate, opt => opt.MapFrom(src => src.RegisterationDate))
            .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate))
            .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.AccountStatus));

            CreateMap<AddressDTO, AddressEntity>();

            CreateMap<SPRegRequestEntity, SPRegApplicationDisplayDTO>()
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business.BusinessName))
                .ForMember(dest => dest.BusinessPhoneNumber, opt => opt.MapFrom(src => src.Business.ContactInformation.PhoneNumber ))
                .ForMember(dest => dest.ServiceProviderPhoneNumber, opt => opt.MapFrom(src => src.Business.ServiceProvider.Account.PhoneNumber))
                .ForMember(dest => dest.ServiceProviderEmail, opt => opt.MapFrom(src => src.Business.ServiceProvider.Account.Email))
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Business.ServiceProvider.Account.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Business.ServiceProvider.Account.UserName))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business.BusinessName))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Response));

            CreateMap<PersonDTO, PersonEntity>()
                 .ForMember(dest => dest.PersonID, opt => opt.MapFrom(src => src.PersonID ?? 0))
                 .ForMember(dest => dest.NationalID, opt => opt.MapFrom(src => src.NationalID))
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.HasValue ? DateTime.SpecifyKind(src.BirthDate.Value, DateTimeKind.Utc) : DateTime.MinValue))
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                 .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.PersonID ?? 0))
                 .ReverseMap();

            CreateMap<CustomerEntity, CustomerDTO>().ReverseMap();

            CreateMap<PassengerEntity, PassengerDTO>()
           .ForMember(dest => dest.PassengerID, opt => opt.MapFrom(src => src.PassengerID))
           .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
           .ReverseMap(); // يدعم التحويل العكسي أيضًا

            CreateMap<PersonEntity, PersonDTO>()
                .ReverseMap();

        }
    }
    }

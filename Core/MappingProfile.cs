using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;
using Core_Layer.Entities.PaymentAccount;

namespace Core_Layer
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
                .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.Address != null ? src.Address.AddressID : (int?)null))
                .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation))
                .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformation != null ? src.ContactInformation.ContactInformationID : (int?)null))
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

            // Payment
            // Map between PaymentAccountDTO and PaymentAccountEntity
            CreateMap<PaymentAccountDTO, PaymentAccountEntity>().ReverseMap();

            // Map between PayPalAccountDTO and PayPalAccountEntity
            CreateMap<PayPalAccountDTO, PayPalAccountEntity>()
                .ForMember(dest => dest.PaymentAccount, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}

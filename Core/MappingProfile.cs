using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Locations;

namespace Core_Layer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterManagerAccountDTO, RegisterAccountDTO>();

            CreateMap<PersonEntity, PersonDTO>();
            CreateMap<ContactInformationEntity, ContactInformationDTO>();
            CreateMap<AddressEntity, AddressDTO>();
            CreateMap<PersonEntity, PersonDTO>().ReverseMap();
            CreateMap<ContactInformationEntity, ContactInformationDTO>().ReverseMap();
            CreateMap<AddressEntity, AddressDTO>().ReverseMap();

            CreateMap<SPRegRequestDTO, SPRegRequestEntity>()
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business))
                .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.Business.BusinessID));
            CreateMap<BusinessDTO, BusinessEntity>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.Address != null ? src.Address.AddressID : (int?)null))
                .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation))
                .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformation != null ? src.ContactInformation.ContactInformationID : (int?)null));


            CreateMap<SPRegRequestEntity, SPRegRequestDTO>()
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business));

            // تعيين من BusinessEntity إلى BusinessDTO
            CreateMap<BusinessEntity, BusinessDTO>()
                .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation));

            // تعيين من BusinessDTO إلى BusinessEntity
            CreateMap<BusinessDTO, BusinessEntity>()
                .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));


            CreateMap<SPRegResponseDTO, SPRegResponseEntity>()
            .ForMember(dest => dest.ResponseID, opt => opt.MapFrom(src => src.ResponseID))
            .ForMember(dest => dest.ResponseText, opt => opt.MapFrom(src => src.ResponseText))
            .ForMember(dest => dest.ResponseDate, opt => opt.MapFrom(src => src.ResponseDate))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dest => dest.RequestID, opt => opt.MapFrom(src => src.RequestID))
            .ForMember(dest => dest.RespondedByID, opt => opt.MapFrom(src => src.RespondedByID))
            .ForMember(dest => dest.RespondedBy, opt => opt.Ignore()) 
            .ForMember(dest => dest.Request, opt => opt.Ignore());

            CreateMap<SPRegResponseDTO, SPRegResponseEntity>()
            .ForMember(dest => dest.ResponseID, opt => opt.MapFrom(src => src.ResponseID))
            .ForMember(dest => dest.ResponseText, opt => opt.MapFrom(src => src.ResponseText))
            .ForMember(dest => dest.ResponseDate, opt => opt.MapFrom(src => src.ResponseDate))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dest => dest.RequestID, opt => opt.MapFrom(src => src.RequestID))
            .ForMember(dest => dest.RespondedByID, opt => opt.MapFrom(src => src.RespondedByID))
            .ForMember(dest => dest.RespondedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Request, opt => opt.Ignore()).ReverseMap();

            CreateMap<RegisterServiceProviderDTO, ServiceProviderEntity>()
                .ForMember(dest => dest.ServiceProviderID, opt => opt.MapFrom(src => src.ServiceProviderID))
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account != null ? src.Account.AccountId : (string?)null))
                .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.BusinessID))
                .ForMember(dest => dest.Business, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore());

            CreateMap<RegisterServiceProviderDTO, ServiceProviderEntity>()
                .ForMember(dest => dest.ServiceProviderID, opt => opt.MapFrom(src => src.ServiceProviderID))
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account != null ? src.Account.AccountId : (string?)null))
                .ForMember(dest => dest.BusinessID, opt => opt.MapFrom(src => src.BusinessID)).ReverseMap();

            CreateMap<RegisterManagerAccountDTO, ManagerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.CreatedByID, opt => opt.MapFrom(src => src.CreatedByID))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Managers, opt => opt.Ignore());

            CreateMap<RegisterManagerAccountDTO, ManagerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.CreatedByID, opt => opt.MapFrom(src => src.CreatedByID))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Managers, opt => opt.Ignore()).ReverseMap();

            CreateMap<RegisterCustomerAccountDTO, CustomerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.Address.AddressID))
                .ForMember(dest => dest.PersonID, opt => opt.MapFrom(src => src.Person.PersonID))
                .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformation.ContactInformationID));

            CreateMap<RegisterCustomerAccountDTO, CustomerEntity>()
                .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.Account.AccountId))
                .ForMember(dest => dest.AddressID, opt => opt.MapFrom(src => src.Address.AddressID))
                .ForMember(dest => dest.PersonID, opt => opt.MapFrom(src => src.Person.PersonID))
                .ForMember(dest => dest.ContactInformationID, opt => opt.MapFrom(src => src.ContactInformation.ContactInformationID))
                .ReverseMap();

            CreateMap<AuthoUser, RegisterAccountDTO>();
            CreateMap<AuthoUser, RegisterAccountDTO>().ReverseMap();

        }
    }
}

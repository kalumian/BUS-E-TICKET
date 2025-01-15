using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Locations;

namespace Core_Layer.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterManagerAccountDTO, RegisterAccountDTO>();

            CreateMap<PersonEntity, PersonDTO>();
            CreateMap<ContactInformationEntity, ContactInformationDTO>();
            CreateMap<AddressEntity, AddressDTO>();

            CreateMap<PersonDTO , PersonEntity>();
            CreateMap<ContactInformationDTO,  ContactInformationEntity > ();
            CreateMap<AddressDTO , AddressEntity>();
        }
    }
}

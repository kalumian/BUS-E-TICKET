using AutoMapper;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class ContactInformationService(IUnitOfWork unitOfWork, IMapper mapper) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        public async Task<ContactInformationEntity> CreateEntityAsync(ContactInformationDTO contactInformationDTO)
        {
            return await CreateEntityAsync(_mapper.Map<ContactInformationEntity>(contactInformationDTO), true); 
        }
        public void UpdateEntity(ContactInformationEntity contactInformationEntity, ContactInformationDTO contactInformationDTO)
        {
            _mapper.Map(contactInformationDTO, contactInformationEntity);
            UpdateEntity(contactInformationEntity);
        }
    }
}

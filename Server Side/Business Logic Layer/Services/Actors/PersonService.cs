using AutoMapper;
using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
using Core_Layer.Entities.Actors;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Actors
{
    public class PersonService(IUnitOfWork unitOfWork, IMapper mapper, ContactInformationService contactInformationService) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        private readonly ContactInformationService _contactInformationService = contactInformationService;
        public async Task<PersonEntity> GetAndUpdateOrCreatePersonWithContactAsync(PersonDTO personDTO)
        {
            var personEntity = await _unitOfWork.People.GetAllQueryable().FirstOrDefaultAsync(p => p.NationalID == personDTO.NationalID);

            if (personEntity == null)
            {
                personEntity = await CreateEntityAsync(personDTO);
            }
            else
            {
                personDTO.PersonID = personEntity.PersonID;
                UpdateEntity(personEntity, personDTO);

                var contactInformationEntity = await _unitOfWork.ContactInformations.GetAllQueryable().Include(c => c.Person).FirstOrDefaultAsync(i=> i.ContactInformationID == personEntity.ContactInformationID);
                personDTO.ContactInformation.ContactInformationID = contactInformationEntity.ContactInformationID;
                _contactInformationService.UpdateEntity(contactInformationEntity, personDTO.ContactInformation);

            }

            return personEntity;
        }
        public async Task<PersonDTO> GetPersonAsync(string NationalID)
        {
            var personEntity = await _unitOfWork.People.GetAllQueryable().Include(p=>p.ContactInformation).FirstOrDefaultAsync(i => i.NationalID == NationalID);
            return _mapper.Map<PersonDTO>(personEntity);
        }

        public async Task<PersonEntity> CreateEntityAsync(PersonDTO personDTO)
        {
            
            var personEntity = _mapper.Map<PersonEntity>(personDTO);
            personEntity.ContactInformation = await _contactInformationService.CreateEntityAsync(personDTO.ContactInformation);
            await CreateEntityAsync(personEntity, saveChanges: true);
            return personEntity;
        }

        public void UpdateEntity(PersonEntity personEntity,PersonDTO personDTO) {
            _mapper.Map(personDTO, personEntity);
            UpdateEntity(personEntity);
        }
    }
}

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
    public class PersonService(IUnitOfWork unitOfWork, IMapper mapper) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        public async Task<PersonEntity> GetAndUpdateOrCreatePersonWithContactAsync(PersonDTO personDTO)
        {
            var personEntity = await _unitOfWork.People.GetAllQueryable().FirstOrDefaultAsync(p => p.NationalID == personDTO.NationalID);

            if (personEntity == null)
            {
                ContactInformationEntity contactInformationEntity = new() { PhoneNumber = personDTO.ContactInformation.PhoneNumber, Email = personDTO.ContactInformation.Email };
                personEntity = _mapper.Map<PersonEntity>(personDTO);
                personEntity.ContactInformation = contactInformationEntity;
                await CreateEntityAsync(personEntity);
            }
            else
            {
                _mapper.Map(personDTO, personEntity);
                UpdateEntity(personEntity);

                var contactInformationEntity = await _unitOfWork.ContactInformations.GetAllQueryable().Include(c => c.Person).FirstOrDefaultAsync(i=> i.ContactInformationID == personDTO.ContactInformation.ContactInformationID);
                _mapper.Map(personDTO.ContactInformation, contactInformationEntity);

                UpdateEntity(contactInformationEntity);

            }

            return personEntity;
        }
        public async Task<PersonDTO> GetPersonAsync(string NationalID)
        {
            var personEntity = await _unitOfWork.People.GetAllQueryable().Include(p=>p.ContactInformation).FirstOrDefaultAsync(i => i.NationalID == NationalID);
            return _mapper.Map<PersonDTO>(personEntity);
        }

    }
}

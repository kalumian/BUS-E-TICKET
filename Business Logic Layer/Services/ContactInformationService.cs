using Business_Logic_Layer.Utilities;
using Core_Layer.DTOs;
using Core_Layer.Entities;
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
    public class ContactInformationService : GeneralService
    {
        public ContactInformationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<int> CreateEntity(ContactInformationDTO contactInformationDTO)
        {

            var contact = new ContactInformationEntity()
            {
                MobileNumber2 = contactInformationDTO.MobileNumber2,
                Instagram = contactInformationDTO.Instagram,
                Twitter = contactInformationDTO.Twitter,
                Facebook = contactInformationDTO.Facebook,
                LinkedIn = contactInformationDTO.LinkedIn,
                LandLineNumber = contactInformationDTO.LandLineNumber
            };
            // Entity Validation 
            ValidationHelper.ValidateEntity(contact);

            await _unitOfWork.ContactInformations.AddAsync(contact);
            await _unitOfWork.SaveChangesAsync();
            CheckCreatedState<ContactInformationEntity>(contact.ContactInformationID);
            return contact.ContactInformationID;
        }
    }
}

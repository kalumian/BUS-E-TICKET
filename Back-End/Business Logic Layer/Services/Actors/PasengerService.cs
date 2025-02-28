using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Data_Access_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Actors
{
    public class PasengerService(IUnitOfWork unitOfWork, IMapper mapper) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;
        public async Task<PassengerEntity> GetOrCreatePassengerAsync(PersonEntity person)
        {
            var passenger = await _unitOfWork.Passengers.FirstOrDefaultAsync(p => p.PersonID == person.PersonID);

            if (passenger == null)
            {
                passenger = new PassengerEntity { Person = person };
                await CreateEntityAsync(passenger);
            }
            else
            {

            }

            return passenger;
        }
        public IEnumerable<PassengerDTO> GetAll()
        {
            var passenger = _unitOfWork.Passengers.GetAllQueryable().Include(p => p.Person).ThenInclude(p => p.ContactInformation).ToList();
            return _mapper.Map<IEnumerable<PassengerDTO>>(passenger);
        }
        public PassengerDTO? GetById(string nationalID)
        {
            var passenger = _unitOfWork.Passengers.GetAllQueryable()
                .Include(p => p.Person)
                .ThenInclude(p => p.ContactInformation)
                .FirstOrDefault(p => p.Person.NationalID == nationalID);

            return _mapper.Map<PassengerDTO>(passenger);
        }
    }
}

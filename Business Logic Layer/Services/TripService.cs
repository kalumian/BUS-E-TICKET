using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities.Trip;
using Core_Layer.Entities.Locations;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.Entities.Actors.ServiceProvider;
using Business_Logic_Layer.Services.Actors.ServiceProvider;

namespace Business_Logic_Layer.Services
{
    public class TripService(IUnitOfWork unitOfWork, IMapper mapper, ServiceProviderService serviceProviderService) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        public async Task<TripRegistrationDTO> AddTripAsync(TripRegistrationDTO tripDTO)
        {
            serviceProviderService.EnsureServiceProvider();
            // التحقق من موفر الخدمة
            CheckEntityExist<ServiceProviderEntity>(sp => sp.ServiceProviderID == tripDTO.ServiceProviderID);

            // التحقق من صحة المواقع (StartLocation و EndLocation)
            var startLocation = await HandleLocationAsync(tripDTO.StartLocation);
            var endLocation = await HandleLocationAsync(tripDTO.EndLocation);

            // التحقق من القيم المنطقية
            if (tripDTO.EndDate <= tripDTO.StartDate)
                throw new BadRequestException("End date must be greater than start date.");
            if (tripDTO.TotalSeats <= 0)
                throw new BadRequestException("Total seats must be greater than 0.");

            // تحويل الكيان
            var tripEntity = _mapper.Map<TripEntity>(tripDTO);
            tripEntity.StartLocationID = startLocation.LocationID;
            tripEntity.EndLocationID = endLocation.LocationID;

            // إنشاء الرحلة
            var createdTrip = await CreateEntityAsync(tripEntity, saveChanges: true);

            // إعادة النتيجة
            var resultDTO = _mapper.Map<TripRegistrationDTO>(createdTrip);
            resultDTO.StartLocation = tripDTO.StartLocation;
            resultDTO.EndLocation = tripDTO.EndLocation;

            return resultDTO;
        }

        private async Task<LocationEntity> HandleLocationAsync(LocationDTO locationDTO)
        {
            // التحقق من الموقع الموجود أو إنشاؤه
            if (locationDTO.LocationID.HasValue)
            {
                var location = _unitOfWork.Locations.GetById(locationDTO.LocationID.Value);
                return location ?? throw new NotFoundException($"Location with ID {locationDTO.LocationID} not found.");
            }

            // إنشاء الموقع الجديد
            var locationEntity = _mapper.Map<LocationEntity>(locationDTO);
            var createdLocation = await CreateEntityAsync(locationEntity, saveChanges: true);

            return createdLocation;
        }
    }
}

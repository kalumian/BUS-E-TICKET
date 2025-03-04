using AutoMapper;
using Core_Layer.DTOs;
using Core_Layer.Entities.Trip;
using Core_Layer.Entities.Locations;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.Entities.Actors.ServiceProvider;
using Business_Logic_Layer.Services.Actors.ServiceProvider;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Core_Layer.Enums;

namespace Business_Logic_Layer.Services
{
    public class TripService(IUnitOfWork unitOfWork, IMapper mapper, ServiceProviderService serviceProviderService) : GeneralService(unitOfWork)
    {
        private readonly IMapper  _mapper = mapper;
        private readonly ServiceProviderService _serviceProviderService = serviceProviderService;

        public async Task<bool> AddTripAsync(TripRegistrationDTO tripDTO)
        {
            _serviceProviderService.EnsureServiceProvider();
            await _serviceProviderService.EnsureOwnershipAsync(tripDTO.ServiceProviderID);

            // بدء الترانزكشن
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // التحقق من صحة المواقع (StartLocation و EndLocation) وإنشاؤها مع العناوين
                var startLocation = await HandleLocationWithAddressAsync(tripDTO.StartLocation);
                var endLocation = await HandleLocationWithAddressAsync(tripDTO.EndLocation);

                // التحقق من أن الموقعين أو العناوين أو المدن مختلفان
                if (startLocation.LocationID == endLocation.LocationID ||
                    startLocation.AddressID == endLocation.AddressID ||
                    startLocation.Address.CityID == endLocation.Address.CityID)
                {
                    throw new BadRequestException("Start location and end location cannot be the same or have the same address or city.");
                }

                
                if (tripDTO.TotalSeats <= 0)
                    throw new BadRequestException("Total seats must be greater than 0.");

                var ServiceProviderCreatrer = _unitOfWork.ServiceProviders.FirstOrDefault(i => i.AccountID == tripDTO.ServiceProviderID) ??
                     throw new BadRequestException("Service Provider Id Is Wrong");

                tripDTO.ServiceProviderID = ServiceProviderCreatrer.ServiceProviderID.ToString();

                var tripEntity = _mapper.Map<TripEntity>(tripDTO);
                tripEntity.StartLocationID = startLocation.LocationID;
                tripEntity.EndLocationID = endLocation.LocationID;
                tripEntity.TripDuration = TimeSpan.FromMinutes(Convert.ToDouble(tripDTO.TripDuration));
                var createdTrip = await CreateEntityAsync(tripEntity, saveChanges: true);

                // تأكيد الترانزكشن
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                // إلغاء الترانزكشن عند حدوث خطأ
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<LocationEntity> HandleLocationWithAddressAsync(LocationDTO locationDTO)
        {
            if (locationDTO.LocationID.HasValue)
            {
                var location = _unitOfWork.Locations.GetById(locationDTO.LocationID.Value);
                return location ?? throw new NotFoundException($"Location with ID {locationDTO.LocationID} not found.");
            }

            // التحقق من AddressID داخل AddressDTO أو إنشاء عنوان جديد
            AddressEntity address;
            if (locationDTO.Address?.AddressID.HasValue == true)
            {
                address = _unitOfWork.Addresses.GetById(locationDTO.Address.AddressID.Value);
                if (address == null)
                    throw new NotFoundException($"Address with ID {locationDTO.Address.AddressID.Value} not found.");
            }
            else
            {
                // إنشاء عنوان جديد باستخدام AddressDTO
                if (locationDTO.Address == null)
                    throw new BadRequestException("Address information is required.");
                address = mapper.Map<AddressEntity>(locationDTO.Address);
                address = await CreateEntityAsync(address, saveChanges: true);
            }

            // إنشاء الموقع وربطه بالعنوان
            var locationEntity = mapper.Map<LocationEntity>(locationDTO);
            locationEntity.AddressID = address.AddressID;
            var createdLocation = await CreateEntityAsync(locationEntity, saveChanges: true);

            return createdLocation;
        }
        public async Task<List<TripDisplayDTO>> GetTripsByProviderAsync(string? providerId)
        {
            var query = _unitOfWork.Trips.GetAllQueryable();

            // تطبيق شرط فلترة providerId إذا كان موجودًا قبل إضافة Includes
            if (!string.IsNullOrEmpty(providerId))
            {
                if (await _unitOfWork.ServiceProviders.AnyAsync(i=>i.AccountID == providerId))
                {
                    query = query.Where(t => t.ServiceProvider.AccountID == providerId);
                }
            }

            query = query
                .Include(t => t.Currency)
                .Include(t => t.ServiceProvider)
                    .ThenInclude(sp => sp.Business)
                .Include(t => t.StartLocation)
                    .ThenInclude(sl => sl.Address)
                        .ThenInclude(a => a.City)
                .Include(t => t.EndLocation)
                    .ThenInclude(el => el.Address)
                        .ThenInclude(a => a.City);

            var trips = await query.ToListAsync();
            return _mapper.Map<List<TripDisplayDTO>>(trips);
        }
        public async Task<TripDisplayDTO?> GetTripByIdAsync(int tripId)
        {
            var trip = await _unitOfWork.Trips.GetAllQueryable()
                .Include(t => t.Currency)
                .Include(t => t.ServiceProvider)
                    .ThenInclude(sp => sp.Business)
                .Include(t => t.StartLocation)
                    .ThenInclude(sl => sl.Address)
                        .ThenInclude(a => a.City)
                .Include(t => t.EndLocation)
                    .ThenInclude(el => el.Address)
                        .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(t => t.TripID == tripId);

            return _mapper.Map<TripDisplayDTO>(trip);
        }

        public async Task<List<TripDisplayDTO>> SearchTripsByCitiesAndDate(int fromCityId, int toCityId, DateTime tripDate)
        {
            var trips = await _unitOfWork.Trips.GetAllQueryable()
                .Include(t => t.Currency)
                .Include(t => t.ServiceProvider)
                    .ThenInclude(sp => sp.Business)
                .Include(t => t.StartLocation)
                    .ThenInclude(sl => sl.Address)
                        .ThenInclude(a => a.City)
                .Include(t => t.EndLocation)
                    .ThenInclude(el => el.Address)
                        .ThenInclude(a => a.City)
                .Where(t => t.StartLocation.Address.City.CityID == fromCityId &&
                            t.EndLocation.Address.City.CityID == toCityId &&
                            t.TripDate.Date == tripDate.Date)
                .ToListAsync();

            return _mapper.Map<List<TripDisplayDTO>>(trips);
        }
        public bool IsThereAbilableSeat(int TripID, int VehicleCapacity)
        {
            return GetTripBookingsCount(TripID) < VehicleCapacity;
        }
        private int GetTripBookingsCount(int TripID)
        {
            return _unitOfWork.Reservations.GetAll()
                .Where(i => i.TripID == TripID && (i.ReservationStatus == EnReservationStatus.Completed || i.ReservationStatus == EnReservationStatus.Pending))
                .Count(); ;
        }
    }
}

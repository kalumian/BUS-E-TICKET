using AutoMapper;
using Business_Logic_Layer.Services.Actors;
using Business_Logic_Layer.Services.Payment;
using Business_Logic_Layer.Services;
using Core_Layer.DTOs;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Trip.Reservation;
using Core_Layer.Entities.Trip;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Core_Layer.Interfaces.Payment;
using Data_Access_Layer.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public class ReservationService(IUnitOfWork unitOfWork, IMapper mapper, PayPalService payPalService, BaseUserService baseUserService, IServiceScopeFactory serviceScopeFactory,PaymentService paymentService) : GeneralService(unitOfWork)
{
    private readonly IMapper _mapper = mapper;
    private readonly PaymentService _paymentService = paymentService;

    public async Task<object> CreateReservationAsync(CreateReservationDTO reservationDTO, string baseUrl)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(reservationDTO.TripID)
                ?? throw new NotFoundException($"Trip with ID {reservationDTO.TripID} not found.");

            if (IsThereAbilableSeat(trip))
                throw new BadRequestException("No available seats for this trip.");

            var person = await GetOrCreatePersonAsync(reservationDTO.Passenger.Person);
            var passenger = await GetOrCreatePassengerAsync(person);

            await HasPreviousReservationAsync(person.NationalID);

            var reservation = new ReservationEntity
            {
                Passenger = passenger,
                TripID = reservationDTO.TripID,
                ReservationStatus = EnReservationStatus.Pending,
                ReservationDate = DateTime.UtcNow,
            };

            await CreateEntityAsync(reservation, saveChanges:true);
            var paymentLink = await _paymentService.ProcessPaymentAsync(reservation, baseUrl, reservationDTO.PaymentMethod);


            if (string.IsNullOrEmpty(paymentLink))
            {
                throw new InvalidOperationException("Payment failed. Please try again.");
            }

            await transaction.CommitAsync();
            return new
            {
                reservation.ReservationID,
                PaymentLink = paymentLink
            };
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private bool IsThereAbilableSeat(TripEntity trip)
    {
        var ReservationsCount = _unitOfWork.Reservations.GetAll()
            .Where(i => i.TripID == trip.TripID && (i.ReservationStatus == EnReservationStatus.Completed || i.ReservationStatus == EnReservationStatus.Pending))
            .Count();
        var tripCapacity = trip.VehicleCapacity;

        return ReservationsCount < tripCapacity;
    }

    private async Task<PersonEntity> GetOrCreatePersonAsync(PersonDTO personDTO)
    {
        var person = await _unitOfWork.People.FirstOrDefaultAsync(p => p.NationalID == personDTO.NationalID);

        if (person == null)
        {
            person = _mapper.Map<PersonEntity>(personDTO);
            await CreateEntityAsync(person);
        }

        return person;
    }

    private async Task<PassengerEntity> GetOrCreatePassengerAsync(PersonEntity person)
    {
        var passenger = await _unitOfWork.Passengers.FirstOrDefaultAsync(p => p.PersonID == person.PersonID);

        if (passenger == null)
        {
            passenger = new PassengerEntity { Person = person };
            await CreateEntityAsync(passenger);
        }

        return passenger;
    }

    private async Task HasPreviousReservationAsync(string nationalID)
    {
        var previousReservations = await _unitOfWork.Reservations.GetAllQueryable()
        .AnyAsync(r => r.Passenger.Person.NationalID == nationalID && 
        (r.ReservationStatus == EnReservationStatus.Completed || r.ReservationStatus == EnReservationStatus.Pending));

        if (previousReservations)
            throw new BadRequestException($"This person has already made a reservation with National ID {nationalID}.");
    }
}

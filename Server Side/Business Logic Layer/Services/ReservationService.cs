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
using System.Reflection.PortableExecutable;
using Core_Layer.Entities;

public class ReservationService(IUnitOfWork unitOfWork,PersonService personService,PaymentService paymentService, PasengerService pasengerService, TripService tripService) : GeneralService(unitOfWork)
{
    private readonly PaymentService _paymentService = paymentService;
    private readonly PersonService _personService = personService;
    private readonly PasengerService _pasengerService = pasengerService;
    private readonly TripService _tripService= tripService;

    public async Task<object> CreateReservationAsync(CreateReservationDTO reservationDTO, string baseUrl)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var trip = await _unitOfWork.Trips.GetByIdAsync(reservationDTO.TripID)
                ?? throw new NotFoundException($"Trip with ID {reservationDTO.TripID} not found.");

            if (_tripService.IsThereAbilableSeat(trip.TripID, trip.VehicleCapacity))
                throw new BadRequestException("No available seats for this trip.");

            var person = await _personService.GetAndUpdateOrCreatePersonWithContactAsync(reservationDTO.Passenger.Person);
            var passenger = await _pasengerService.GetOrCreatePassengerAsync(person);

            await HasCompletedBookingForTripAsync(reservationDTO);
            await CancelPreviousPendingBookings(reservationDTO);
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

 



    private async Task HasCompletedBookingForTripAsync(CreateReservationDTO booking)
    {
        var CompletedBooking = await _unitOfWork.Reservations.GetAllQueryable()
        .AnyAsync(r => booking.TripID == r.TripID &&  r.Passenger.Person.NationalID == booking.Passenger.Person.NationalID && 
        (r.ReservationStatus == EnReservationStatus.Completed));

        if (CompletedBooking)
            throw new BadRequestException($"This person has already made a reservation with National ID {booking.Passenger.Person.NationalID}.");
    }
    private async Task CancelPreviousPendingBookings(CreateReservationDTO booking)
    {
        var previousBooking = await _unitOfWork.Reservations.GetAllQueryable()
        .FirstOrDefaultAsync(r => booking.TripID == r.TripID && r.Passenger.Person.NationalID == booking.Passenger.Person.NationalID &&
        (r.ReservationStatus == EnReservationStatus.Pending));

        previousBooking.ReservationStatus = EnReservationStatus.Failed;
        _unitOfWork.Reservations.Update(previousBooking);

    }
    public async Task<BookingDTO> GetReservationByPNRAsync(string pnr)
    {
        var ticket = await _unitOfWork.Tickets.GetAllQueryable()
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Trip)
                          .ThenInclude(r => r.StartLocation)
                              .ThenInclude(r => r.Address)
                                  .ThenInclude(r => r.City)
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Trip)
                          .ThenInclude(r => r.EndLocation)
                              .ThenInclude(r => r.Address)
                                  .ThenInclude(r => r.City)
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Passenger)
                            .ThenInclude(r => r.Person)
                              .FirstOrDefaultAsync(t => t.PNR == pnr);

        if (ticket == null) return null;

        var reservation = ticket.Invoice?.Payment?.Reservation;

        return new BookingDTO
        {
            BookingID = reservation?.ReservationID,
            TripID = reservation?.TripID,
            BookingStatus = reservation?.ReservationStatus.ToString(),
            BookingDate = reservation?.ReservationDate ?? DateTime.MinValue,
            PassengerName = $"{reservation?.Passenger?.Person?.FirstName} {reservation?.Passenger?.Person?.LastName}",
            PassengerNationalID = reservation?.Passenger?.Person?.NationalID,
            TripDate = reservation?.Trip?.TripDate ?? DateTime.MinValue,
            FromCity = reservation?.Trip?.StartLocation?.Address?.City?.CityName,
            ToCity = reservation?.Trip?.EndLocation?.Address?.City?.CityName,
        };
    }
    public async Task<IEnumerable<BookingDTO>> GetAllReservationsAsync()
    
    {
        var tickets = await _unitOfWork.Tickets.GetAllQueryable()
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Trip)
                            .ThenInclude(r => r.StartLocation)
                                .ThenInclude(r => r.Address)
                                    .ThenInclude(r => r.City)
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Trip)
                            .ThenInclude(r => r.EndLocation)
                                .ThenInclude(r => r.Address)
                                    .ThenInclude(r => r.City)
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Passenger)
                            .ThenInclude(r => r.Person)
            .ToListAsync();

        return tickets.Select(ticket => new BookingDTO
        {
            BookingID = ticket.Invoice?.Payment?.Reservation?.ReservationID,
            TripID = ticket.Invoice?.Payment?.Reservation?.TripID,
            pnr = ticket.PNR,
            BookingStatus = ticket.Invoice?.Payment?.Reservation?.ReservationStatus.ToString(),
            BookingDate = ticket.Invoice?.Payment?.Reservation?.ReservationDate ?? DateTime.MinValue,
            PassengerName = $"{ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.FirstName} {ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.LastName}",
            PassengerNationalID = ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.NationalID,
            TripDate = ticket.Invoice?.Payment?.Reservation?.Trip?.TripDate ?? DateTime.MinValue,
            FromCity = ticket.Invoice?.Payment?.Reservation?.Trip?.StartLocation?.Address?.City?.CityName,
            ToCity = ticket.Invoice?.Payment?.Reservation?.Trip?.EndLocation?.Address?.City?.CityName,
        }).ToList();
    }
    public async Task<IEnumerable<BookingDTO>> GetReservationsByTripIdAsync(int tripId)
    {
        var tickets = await _unitOfWork.Tickets.GetAllQueryable()
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Trip)
                            .ThenInclude(r => r.StartLocation)
                                .ThenInclude(r => r.Address)
                                    .ThenInclude(r => r.City)
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Trip)
                            .ThenInclude(r => r.EndLocation)
                                .ThenInclude(r => r.Address)
                                    .ThenInclude(r => r.City)
            .Include(t => t.Invoice)
                .ThenInclude(i => i.Payment)
                    .ThenInclude(p => p.Reservation)
                        .ThenInclude(r => r.Passenger)
                            .ThenInclude(r => r.Person)
            .Where(t => t.Invoice.Payment.Reservation.TripID == tripId)
            .ToListAsync();

        return tickets.Select(ticket => new BookingDTO
        {
            BookingID = ticket.Invoice?.Payment?.Reservation?.ReservationID,
            TripID = ticket.Invoice?.Payment?.Reservation?.TripID,
            BookingStatus = ticket.Invoice?.Payment?.Reservation?.ReservationStatus.ToString(),
            BookingDate = ticket.Invoice?.Payment?.Reservation?.ReservationDate ?? DateTime.MinValue,
            PassengerName = $"{ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.FirstName} {ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.LastName}",
            PassengerNationalID = ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.NationalID,
            TripDate = ticket.Invoice?.Payment?.Reservation?.Trip?.TripDate ?? DateTime.MinValue,
            FromCity = ticket.Invoice?.Payment?.Reservation?.Trip?.StartLocation?.Address?.City?.CityName,
            ToCity = ticket.Invoice?.Payment?.Reservation?.Trip?.EndLocation?.Address?.City?.CityName,
            pnr = ticket.PNR
        }).ToList();
    }

}

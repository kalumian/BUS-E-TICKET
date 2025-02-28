using Core_Layer.Entities.Trip.Reservation;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.Exceptions;
using Core_Layer.DTOs;
using Core_Layer.Enums;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Services
{
    public class TicketService(IUnitOfWork unitOfWork) : GeneralService(unitOfWork)
    {
        public async Task<TicketEntity> CreateTicketAsync(InvoiceEntity invoice)
        {
            _ = await _unitOfWork.Invoices.GetByIdAsync(invoice.InvoiceID) ?? throw new NotFoundException("Invoice not found.");

            var ticket = new TicketEntity
            {
                PNR = Guid.NewGuid().ToString()[..10],  
                IssueDate = DateTime.UtcNow,
                Invoice = invoice,
                InvoiceID = invoice.InvoiceID
            };

            await CreateEntityAsync(ticket, saveChanges: true);
            return ticket;
        }
        public async Task<TicketDTO> GetTicketByReservationId(int reservationId)
        {
            var ticket = await _unitOfWork.Tickets.GetAllQueryable()
                 .Include(t => t.Invoice)
                     .ThenInclude(i => i.Payment)
                     .ThenInclude(p => p.Reservation)
                     .ThenInclude(r => r.Trip)
                         .ThenInclude(t => t.Currency)
                 .Include(t => t.Invoice)
                     .ThenInclude(i => i.Payment)
                     .ThenInclude(p => p.Reservation)
                     .ThenInclude(r => r.Trip)
                         .ThenInclude(t => t.ServiceProvider)
                         .ThenInclude(sp => sp.Business)
                 .Include(t => t.Invoice)
                     .ThenInclude(i => i.Payment)
                     .ThenInclude(p => p.Reservation)
                     .ThenInclude(r => r.Trip)
                         .ThenInclude(t => t.StartLocation)
                         .ThenInclude(l => l.Address)
                         .ThenInclude(a => a.City)
                 .Include(t => t.Invoice)
                     .ThenInclude(i => i.Payment)
                     .ThenInclude(p => p.Reservation)
                     .ThenInclude(r => r.Trip)
                         .ThenInclude(t => t.EndLocation)
                         .ThenInclude(l => l.Address)
                         .ThenInclude(a => a.City)
                 .Include(t => t.Invoice)
                     .ThenInclude(i => i.Payment)
                     .ThenInclude(p => p.Reservation)
                     .ThenInclude(r => r.Passenger)
                         .ThenInclude(p => p.Person)
                 .FirstOrDefaultAsync(t => t.Invoice.Payment.ReservationID == reservationId);

            if (ticket == null) return null;

            return new TicketDTO
            {
                TicketID = ticket.TicketID,
                PNR = ticket.PNR,
                IssueDate = ticket.IssueDate,
                InvoiceNumber = ticket.Invoice?.InvoiceNumber,
                InvoiceIssueDate = ticket.Invoice?.IssueDate ?? DateTime.MinValue,
                PaymentMethod = ticket.Invoice?.Payment?.PaymentMethod.ToString(),
                OrderID = ticket.Invoice?.Payment?.OrderID,
                PaymentStatus = ticket.Invoice?.Payment?.PaymentStatus.ToString(),
                IsRefundable = ticket.Invoice?.Payment?.IsRefundable ?? false,
                PaymentDate = ticket.Invoice?.Payment?.PaymentDate ?? DateTime.MinValue,
                VAT = ticket.Invoice?.Payment?.VAT.ToString(),
                TotalAmount = ticket.Invoice?.Payment?.TotalAmount.ToString(),
                DiscountAmount = ticket.Invoice?.Payment?.DiscountAmount.ToString(),
                ReservationDate = ticket.Invoice?.Payment?.Reservation?.ReservationDate ?? DateTime.MinValue,
                VehicleInfo = ticket.Invoice?.Payment?.Reservation?.Trip?.VehicleInfo,
                DriverInfo = ticket.Invoice?.Payment?.Reservation?.Trip?.DriverInfo,
                TripDate = ticket.Invoice?.Payment?.Reservation?.Trip?.TripDate ?? DateTime.MinValue,
                TripAmount = ticket.Invoice?.Payment?.Reservation?.Trip?.Price.ToString(),
                Currency = ticket.Invoice?.Payment?.Reservation?.Trip?.Currency?.CurrencyName,
                TripDuration = ticket.Invoice?.Payment?.Reservation?.Trip?.TripDuration ?? TimeSpan.Zero,
                FromCity = ticket.Invoice?.Payment?.Reservation?.Trip?.StartLocation?.Address?.City?.CityName,
                ToCity = ticket.Invoice?.Payment?.Reservation?.Trip?.EndLocation?.Address?.City?.CityName,
                BusinessName = ticket.Invoice?.Payment?.Reservation?.Trip?.ServiceProvider?.Business?.BusinessName,
                LogoURL = ticket.Invoice?.Payment?.Reservation?.Trip?.ServiceProvider?.Business?.LogoURL,
                PassengerName = ticket.Invoice?.Payment?.Reservation?.Passenger?.Person != null ? $"{ticket.Invoice.Payment.Reservation.Passenger.Person.FirstName} {ticket.Invoice.Payment.Reservation.Passenger.Person.LastName}" : null,
                PassengerNationalID = ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.NationalID,
                PassengerGender = ticket.Invoice?.Payment?.Reservation?.Passenger?.Person?.Gender.ToString()
            };
        }
    }
}

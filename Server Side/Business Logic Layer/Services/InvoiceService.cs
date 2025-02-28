using Core_Layer.Entities.Trip.Reservation;
using Business_Logic_Layer.Services;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Threading.Tasks;
using Core_Layer.Entities.Payment;
using Core_Layer.Exceptions;
using Core_Layer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Services
{
    public class InvoiceService(IUnitOfWork unitOfWork) : GeneralService(unitOfWork)
    {
        public async Task<InvoiceEntity> CreateInvoiceAsync(PaymentEntity payment)
        {
            _ = await _unitOfWork.Payments.GetByIdAsync(payment.PaymentID) ?? throw new NotFoundException("Payment not found.");

            var invoice = new InvoiceEntity
            {
                InvoiceNumber = Guid.NewGuid().ToString(),
                IssueDate = DateTime.UtcNow,
                Payment = payment,
                PaymentID = payment.PaymentID,
            };

            await CreateEntityAsync(invoice, saveChanges: true);
            return invoice;
        }
        public async Task<InvoiceDTO> GetInvoiceByReservationId(int reservationId)
        {
            var invoice = await _unitOfWork.Invoices.GetAllQueryable()
                .Include(i => i.Payment)
                .ThenInclude(p => p.Reservation)
                .ThenInclude(r => r.Trip)
                    .ThenInclude(t => t.Currency)
                .Include(i => i.Payment)
                .ThenInclude(p => p.Reservation)
                .ThenInclude(r => r.Trip)
                    .ThenInclude(t => t.ServiceProvider)
                    .ThenInclude(sp => sp.Business)
                .Include(i => i.Payment)
                .ThenInclude(p => p.Reservation)
                .ThenInclude(r => r.Trip)
                    .ThenInclude(t => t.StartLocation)
                    .ThenInclude(l => l.Address)
                    .ThenInclude(a => a.City)
                .Include(i => i.Payment)
                .ThenInclude(p => p.Reservation)
                .ThenInclude(r => r.Trip)
                    .ThenInclude(t => t.EndLocation)
                    .ThenInclude(l => l.Address)
                    .ThenInclude(a => a.City)
                .Include(i => i.Payment)
                .ThenInclude(p => p.Reservation)
                .ThenInclude(r => r.Passenger)
                    .ThenInclude(p => p.Person)
                .FirstOrDefaultAsync(t => t.Payment.ReservationID == reservationId);

            if (invoice == null) return null;

            return new InvoiceDTO
            {
                InvoiceID = invoice.InvoiceID,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                PaymentMethod = invoice.Payment.PaymentMethod.ToString(),
                OrderID = invoice.Payment.OrderID,
                PaymentStatus = invoice.Payment.PaymentStatus.ToString(),
                IsRefundable = invoice.Payment.IsRefundable,
                PaymentDate = invoice.Payment.PaymentDate,
                VAT = invoice.Payment.VAT.ToString(),
                TotalAmount = invoice.Payment.TotalAmount.ToString(),
                DiscountAmount = invoice.Payment.DiscountAmount.ToString(),
                TripAmount = invoice.Payment.Reservation.Trip.Price.ToString(),
                Currency = invoice.Payment.Reservation.Trip.Currency.CurrencyName,
                BusinessName = invoice.Payment.Reservation.Trip.ServiceProvider.Business.BusinessName,
                LogoURL = invoice.Payment.Reservation.Trip.ServiceProvider.Business.LogoURL
            };
        }

    }
}

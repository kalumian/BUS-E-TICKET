using Core_Layer.Configurations;
using Core_Layer.DTOs;
using Core_Layer.Entities.Payment;
using Core_Layer.Entities.Trip.Reservation;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Core_Layer.Interfaces.Payment;
using Data_Access_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Orders;

namespace Business_Logic_Layer.Services.Payment
{
    public class PaymentService(IUnitOfWork unitOfWork, IOptions<PayPalSettings> payPalSettings, TicketService ticketService, InvoiceService invoiceService) : GeneralService(unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<string> ProcessPaymentAsync(ReservationEntity reservation, string baseUrl, EnPaymentMethod enPaymentMethod)
        {
                var trip = await _unitOfWork.Trips.GetByIdAsync(reservation.TripID)
                    ?? throw new NotFoundException($"Trip with ID {reservation.TripID} not found.");

                decimal vat = trip.Price * 0.15m;
                decimal totalAmount = trip.Price + vat;

                var paymentEntity = new PaymentEntity
                {
                    PaymentMethod = enPaymentMethod,
                    PaymentStatus = EnPaymentStatus.Pending,
                    IsRefundable = true,
                    PaymentDate = DateTime.UtcNow,
                    TripAmount = trip.Price,
                    VAT = vat,
                    TotalAmount = totalAmount,
                    DiscountAmount = 0,
                    Reservation = reservation,
                    ReservationID = reservation.ReservationID,
                    CurrencyID = trip.CurrencyID,
                };

            await CreateEntityAsync(paymentEntity);

                var paymentFactory = new PaymentFactory(GetPaymentMethodByEnum(enPaymentMethod));
                var paymentLink = await paymentFactory.PayAsync(paymentEntity, baseUrl);
                return paymentLink;
        }
        public IPaymentMethod GetPaymentMethodByEnum(EnPaymentMethod Enum)
        {
            switch (Enum)
            {
                case EnPaymentMethod.PayPal:
                    return new PayPalService(payPalSettings, unitOfWork, invoiceService, ticketService);
                default:
                    break;
            }
            return new PayPalService(payPalSettings, unitOfWork, invoiceService, ticketService);
        }
       

    }
}
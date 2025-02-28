using Core_Layer.Configurations;
using Core_Layer.Entities;
using Core_Layer.Entities.Payment;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Core_Layer.Interfaces.Payment;
using Data_Access_Layer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace Business_Logic_Layer.Services.Payment
{
    public class PayPalService(IOptions<PayPalSettings> payPalSettings, IUnitOfWork unitOfWork, InvoiceService invoiceService, TicketService ticketService) : GeneralService(unitOfWork), IPaymentMethod
    {
        private readonly PayPalSettings _payPalSettings = payPalSettings.Value;
        private readonly InvoiceService _invoiceService = invoiceService;
        private readonly TicketService _ticketService = ticketService;

        private PayPalHttpClient GetClient()
        {
            var environment = new SandboxEnvironment(
                _payPalSettings.ClientId,
                _payPalSettings.ClientSecret
            );

            return new PayPalHttpClient(environment);
        }

        public async Task<string> CreatePaymentAsync(PaymentEntity paymentEntity, string BaseUrl)
        {
            var client = GetClient();

            var orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits =
            [
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = _unitOfWork.GetDynamicRepository<CurrencyEntity>().GetById(paymentEntity.CurrencyID).CurrencyCode,
                        Value = paymentEntity.TotalAmount.ToString("F2")
                    }
                }
            ],
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = $"{BaseUrl}payment/confirm?reservationId={paymentEntity.ReservationID}",
                    CancelUrl = $"{BaseUrl}payment/failure"
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(orderRequest);

            var response = await client.Execute(request);
            var result = response.Result<Order>();

            paymentEntity.OrderID = result.Id;
            await _unitOfWork.SaveChangesAsync();

            return result.Links[1].Href; 
        }

        public async Task<object> ExecutePaymentAsync(int reservationId)
        {
            var Payment = await _unitOfWork.Payments
             .GetAllQueryable()
             .FirstOrDefaultAsync(p => p.ReservationID == reservationId && p.PaymentStatus == EnPaymentStatus.Pending) ?? throw new NotFoundException($"Payment in Pending status for Reservation with ID {reservationId} is not found.");
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var client = GetClient();
                var request = new OrdersCaptureRequest(Payment.OrderID);
                request.RequestBody(new OrderActionRequest());

                if (Payment.PaymentDate.AddMinutes(5) < DateTime.UtcNow)
                    throw new InvalidOperationException("Payment confirmation time expired.");

                Payment.PaymentStatus = EnPaymentStatus.Paid;

                var Reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
                if (Reservation != null)
                    Reservation.ReservationStatus = EnReservationStatus.Completed;

                await _unitOfWork.SaveChangesAsync();

                var Invoice = await _invoiceService.CreateInvoiceAsync(Payment);

                var Ticket = await _ticketService.CreateTicketAsync(Invoice);

                var response = await client.Execute(request);
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    throw new InvalidOperationException("Payment capture failed.");


                transaction.Commit();
                return new { Ticket.TicketID, Invoice.InvoiceID}; // can use DTO
            }
            catch
            {
                transaction.Rollback();
                await MakePaymentFaild(Payment);
                await _unitOfWork.SaveChangesAsync();

                throw;
            }
        }
        private async Task MakePaymentFaild(PaymentEntity payment)
        {
            if (payment != null && payment.PaymentStatus != EnPaymentStatus.Failed)
            {
                payment.PaymentStatus = EnPaymentStatus.Failed;
                await _unitOfWork.SaveChangesAsync();
            }

            var reservation = await _unitOfWork.Reservations.GetByIdAsync(payment.ReservationID);
            if (reservation != null)
            {
                reservation.ReservationStatus = EnReservationStatus.Failed;
                await _unitOfWork.SaveChangesAsync();
            }
        }
        public async Task WhenPaymentFaild(int reservationId)
        {
            var payment = await _unitOfWork.Payments
            .GetAllQueryable()
            .FirstOrDefaultAsync(p => p.ReservationID == reservationId && p.PaymentStatus == EnPaymentStatus.Pending);
            await MakePaymentFaild(payment);
           
        }
    }
}

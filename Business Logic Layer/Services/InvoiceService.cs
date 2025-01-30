using Core_Layer.Entities.Trip.Reservation;
using Business_Logic_Layer.Services;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Threading.Tasks;
using Core_Layer.Entities.Payment;
using Core_Layer.Exceptions;

namespace Business_Logic_Layer.Services
{
    public class InvoiceService : GeneralService
    {
        public InvoiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<InvoiceEntity> CreateInvoiceAsync(PaymentEntity payment)
        {
            var paymentEntity = await _unitOfWork.Payments.GetByIdAsync(payment.PaymentID) ?? throw new NotFoundException("Payment not found.");

            var invoice = new InvoiceEntity
            {
                InvoiceNumber = Guid.NewGuid().ToString(),
                IssueDate = DateTime.Now,
                Payment = payment,
                PaymentID = paymentEntity.PaymentID,
            };

            await CreateEntityAsync(invoice, saveChanges: true);
            return invoice;
        }
    }
}

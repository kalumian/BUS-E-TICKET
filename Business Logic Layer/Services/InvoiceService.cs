﻿using Core_Layer.Entities.Trip.Reservation;
using Business_Logic_Layer.Services;
using Data_Access_Layer.UnitOfWork;
using System;
using System.Threading.Tasks;
using Core_Layer.Entities.Payment;
using Core_Layer.Exceptions;

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
                IssueDate = DateTime.Now,
                Payment = payment,
                PaymentID = payment.PaymentID,
            };

            await CreateEntityAsync(invoice, saveChanges: true);
            return invoice;
        }
    }
}

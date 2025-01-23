using Core_Layer.Entities.PaymentAccount;
using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.DTOs;
using AutoMapper;
using Core_Layer.Entities.Actors.ServiceProvider;

namespace Business_Logic_Layer.Services.Payment
{
    public class PaymentAccountService : GeneralService
    {
        private readonly IMapper _mapper;

        public PaymentAccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<PayPalAccountDTO> AddPayPalAccountAsync(PayPalAccountDTO dto)
        {
            // Validate Input
            if (dto.PaymentAccount == null)
                throw new BadRequestException("Payment account details are required.");
            if (string.IsNullOrWhiteSpace(dto.AccountEmail))
                throw new BadRequestException("PayPal email is required.");

            // Ensure Currency, ServiceProvider exist
            CheckEntityExist<CurrencyEntity>(c => c.CurrencyID == dto.PaymentAccount.CurrencyID);
            CheckEntityExist<ServiceProviderEntity>(s => s.ServiceProviderID == dto.PaymentAccount.ServiceProviderID);

            // Map PaymentAccountEntity
            var paymentAccountEntity = _mapper.Map<PaymentAccountEntity>(dto.PaymentAccount);

            // Create PaymentAccount in Database
            var createdPaymentAccount = await CreateEntityAsync(paymentAccountEntity, saveChanges: true);

            // Map PayPalAccountEntity
            var payPalAccountEntity = _mapper.Map<PayPalAccountEntity>(dto);
            payPalAccountEntity.PaymentAccountID = createdPaymentAccount.PaymentAccountID;

            // Create PayPalAccount in Database
            var createdPayPalAccount = await CreateEntityAsync(payPalAccountEntity, saveChanges: true);

            // Map back to PayPalAccountDTO
            var resultDto = _mapper.Map<PayPalAccountDTO>(createdPayPalAccount);

            return resultDto;
        }
    }
}

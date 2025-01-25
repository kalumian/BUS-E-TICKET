using Core_Layer.Enums;
using Core_Layer.Exceptions;
using Data_Access_Layer.UnitOfWork;
using Core_Layer.DTOs;
using AutoMapper;
using Core_Layer.Entities.Actors.ServiceProvider;
using Microsoft.EntityFrameworkCore;
using Core_Layer.Entities.Actors.ServiceProvider.PaymentAccount;
using Core_Layer.Entities;

namespace Business_Logic_Layer.Services.Payment
{
    public class PaymentAccountService(IUnitOfWork unitOfWork, IMapper mapper) : GeneralService(unitOfWork)
    {
        private readonly IMapper _mapper = mapper;

        public async Task<PayPalAccountDTO> AddPayPalAccountAsync(PayPalAccountDTO dto)
        {
            // Validate Input
            if (dto.PaymentAccount == null)
                throw new BadRequestException("Payment account details are required.");
            if (string.IsNullOrWhiteSpace(dto.AccountEmail))
                throw new BadRequestException("PayPal email is required.");

            // Check if the email already exists in PayPalAccountEntity
            bool emailExists = await _unitOfWork.GetDynamicRepository<PayPalAccountEntity>()
                .AnyAsync(p => p.AccountEmail == dto.AccountEmail);

            if (emailExists)
                throw new BadRequestException($"The email {dto.AccountEmail} is already associated with another PayPal account.");

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
        public async Task<object> GetAllAccountsByServiceProviderAsync(int serviceProviderId)
        {
            // التحقق من وجود موفر الخدمة
            CheckEntityExist<ServiceProviderEntity>(s => s.ServiceProviderID == serviceProviderId);

            // جلب جميع حسابات الدفع الخاصة بموفر الخدمة
            var paymentAccounts = _unitOfWork.GetDynamicRepository<PaymentAccountEntity>()
                .GetAllQueryable()
                .Where(p => p.ServiceProviderID == serviceProviderId)
                .Include(p => p.Currency) // تضمين العملة
                .ToList(); // تنفيذ الاستعلام هنا

            // استخراج معرفات الحسابات
            var paymentAccountIds = paymentAccounts.Select(pa => pa.PaymentAccountID).ToList();

            // جلب جميع حسابات PayPal المرتبطة بهذه الحسابات
            var payPalAccounts = _unitOfWork.GetDynamicRepository<PayPalAccountEntity>()
                .GetAllQueryable()
                .Where(pp => paymentAccountIds.Contains(pp.PaymentAccountID)) // استخدام Contains بدلاً من Any
                .ToList(); // تنفيذ الاستعلام هنا

            // تنظيم البيانات للإرجاع
            return new
            {
                Accounts = new
                {
                    PayPal = payPalAccounts.Select(pp => new
                    {
                        pp.PayPalAccountID,
                        pp.AccountEmail,
                        PaymentAccount = paymentAccounts.FirstOrDefault(pa => pa.PaymentAccountID == pp.PaymentAccountID)?.AccountOwnerName,
                        Currency = paymentAccounts.FirstOrDefault(pa => pa.PaymentAccountID == pp.PaymentAccountID)?.Currency?.CurrencyName ?? "Unknown"
                    })
                }
            };
        }


    }
}

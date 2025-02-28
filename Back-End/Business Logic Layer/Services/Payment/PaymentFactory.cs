using Core_Layer.Entities.Payment;
using Core_Layer.Interfaces.Payment;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Payment
{
    public class PaymentFactory(IPaymentMethod paymentMethod)
    {
        private readonly IPaymentMethod _paymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));

        public async Task<string> PayAsync(PaymentEntity paymentEntity, string baseUrl)
        {
            return await _paymentMethod.CreatePaymentAsync(paymentEntity, baseUrl);
        }
    }
}

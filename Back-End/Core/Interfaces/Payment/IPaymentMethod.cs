using Core_Layer.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Interfaces.Payment
{
    public interface IPaymentMethod
    {
        Task<string> CreatePaymentAsync(PaymentEntity paymentEntity, string BaseUrl);
    }
}

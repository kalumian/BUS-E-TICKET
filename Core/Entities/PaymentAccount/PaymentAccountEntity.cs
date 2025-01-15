using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities.Actors.ServiceProvider;

namespace Core_Layer.Entities.PaymentAccount
{
    public class PaymentAccountEntity
    {
        #region Primary Properties

        [Key]
        public int PaymentAccountID { get; set; }

        [Required(ErrorMessage = "PaymentState is required.")]
        public EnPaymentAccountStatus PaymentStatus { get; set; }

        [Required(ErrorMessage = "ReservationDate is required.")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "AccountOwnerName is required.")]
        [MaxLength(100, ErrorMessage = "AccountOwnerName cannot exceed 100 characters.")]
        public required string AccountOwnerName { get; set; }
        #endregion

        #region Foreign Keys

        [ForeignKey("ServiceProvider")]
        public int ServiceProviderID { get; set; }

        [ForeignKey("PaymentAccountType")]
        public int PaymentAccountType_ID { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyID { get; set; }

        #endregion

        #region Navigation Properties

        public ServiceProviderEntity? ServiceProvider { get; set; }
        public CurrencyEntity? Currency { get; set; }

        #endregion
    }
}

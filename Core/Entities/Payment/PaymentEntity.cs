using Core_Layer.Entities.Reservation;
using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Payment
{
    public class PaymentEntity
    {
        #region Properties

        [Key]
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "Payment method is required.")]
        public EnPaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "Payment status is required.")]
        public EnPaymentStatus PaymentStatus { get; set; }
        [Required(ErrorMessage = "IsRefundable is required.")]
        public bool IsRefundable { get; set; }
        [Required(ErrorMessage = "Payment date is required.")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        #endregion

        #region Foregin Keys

        [Required(ErrorMessage = "Reservation ID is required.")]
        [ForeignKey("Reservation")]
        public required int ReservationID { get; set; }

        [Required(ErrorMessage = "Payment Info ID is required.")]
        [ForeignKey("PaymentInfo")]
        public required int PaymentInfoID { get; set; }

        #endregion

        #region Navigation Properties

        public ReservationEntity? Reservation { get; set; }

        public PaymentInfoEntity? PaymentInfo { get; set; }

        #endregion
    }
}

using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities.Trip.Reservation;

namespace Core_Layer.Entities.Payment
{
    public class PaymentEntity
    {
        #region Properties

        [Key]
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "Payment method is required.")]
        public EnPaymentMethod PaymentMethod { get; set; }
        [MaxLength(300,ErrorMessage = "The MaxLength is 300")]
        public string OrderID { get; set; }
        [Required(ErrorMessage = "Payment status is required.")]
        public EnPaymentStatus PaymentStatus { get; set; }
        [Required(ErrorMessage = "IsRefundable is required.")]
        public bool IsRefundable { get; set; }
        [Required(ErrorMessage = "Payment date is required.")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Trip amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Trip amount must be a positive value.")]
        public decimal TripAmount { get; set; }

        [Required(ErrorMessage = "VAT is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "VAT must be a positive value.")]
        public decimal VAT { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "TotalAmount amount must be a positive value.")]
        public decimal TotalAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount amount must be a positive value.")]
        public decimal DiscountAmount { get; set; } = 0;


        #endregion

        #region Foregin Keys

        [Required(ErrorMessage = "Reservation ID is required.")]
        [ForeignKey("Reservation")]
        public int? ReservationID { get; set; }

        [Required(ErrorMessage = "Currency ID is required.")]
        [ForeignKey("Currency")]
        public int? CurrencyID { get; set; }

        #endregion

        #region Navigation Properties

        public ReservationEntity? Reservation { get; set; }
        public CurrencyEntity? Currency { get; set; }
        public InvoiceEntity? Invoice { get; set; }
        #endregion
    }
}

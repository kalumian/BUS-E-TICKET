using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Payment
{
    public class PaymentInfoEntity
    {
        #region  Properties
        [Key]
        public int PaymentInfoID { get; set; }

        [Required(ErrorMessage = "Trip amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Trip amount must be a positive value.")]
        public decimal TripAmount { get; set; }

        [Required(ErrorMessage = "VAT is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "VAT must be a positive value.")]
        public decimal VAT { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Additional amount must be a positive value.")]
        public decimal? AdditionalAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount amount must be a positive value.")]
        public decimal? DiscountAmount { get; set; }

        #endregion
    }
}

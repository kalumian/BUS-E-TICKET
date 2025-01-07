using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities.Locations;

namespace Core_Layer.Entities.PaymentAccount
{
    public class PayPalAccountEntity
    {
        #region Properties

        [Key]
        public int PayPalAccountID { get; set; }

        [Required(ErrorMessage = "AccountEmail is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(250, ErrorMessage = "AccountEmail cannot exceed 250 characters.")]
        public required string AccountEmail { get; set; }

        #endregion

        #region Foreign Keys

        [Required(ErrorMessage = "PaymentAccountID is required.")]
        [ForeignKey("PaymentAccount")]
        public required int PaymentAccountID { get; set; }

        [Required(ErrorMessage = "CountryID is required.")]
        [ForeignKey("Country")]
        public required int CountryID { get; set; }

        #endregion

        #region Navigation Properties

        public PaymentAccountEntity? PaymentAccount { get; set; }
        public CountryEntity? Country { get; set; }


        #endregion
    }
}

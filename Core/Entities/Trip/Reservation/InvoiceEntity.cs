using Core_Layer.Entities.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Trip.Reservation
{
    public class InvoiceEntity
    {
        #region Properties

        [Key]
        public int InvoiceID { get; set; }
        [Required(ErrorMessage = "Issue date is required.")]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        #endregion

        #region Foreing Key

        [Required(ErrorMessage = "Payment ID is required.")]
        [ForeignKey("Payment")]
        public required int PaymentID { get; set; }

        #endregion

        #region Navigation Properties

        public required PaymentEntity Payment { get; set; }

        public ICollection<TicketEntity>? Tickets { get; set; }

        #endregion
    }
}

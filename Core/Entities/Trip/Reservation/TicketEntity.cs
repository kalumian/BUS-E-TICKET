using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core_Layer.Entities.Trip.Reservation
{
    public class TicketEntity
    {
        #region Properties

        [Key]
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Ticket code is required.")]
        [MaxLength(50, ErrorMessage = "Ticket code cannot exceed 50 characters.")]
        public string TicketCode { get; set; } = string.Empty;


        [Required(ErrorMessage = "Issue date is required.")]
        public DateTime IssueDate { get; set; } = DateTime.Now;
        #endregion

        #region Foreing Key

        [Required(ErrorMessage = "Invoice ID is required.")]
        [ForeignKey("Invoice")]
        public required int InvoiceID { get; set; }

        #endregion

        #region Navigation Properties

        public required InvoiceEntity Invoice { get; set; }

        #endregion
    }
}

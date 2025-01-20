using Core_Layer.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request
{
    public class SPRegRequestEntity
    {
        [Key]
        public int SPRegRequestID { get; set; }
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public EnRegisterationRequestStatus Status { get; set; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        // Foregin Keys
        [ForeignKey("Business")]
        [Required(ErrorMessage = "BusinessID is required.")]
        public int BusinessID { get; set; }

        // Navigation Properties
        public BusinessEntity? Business { get; set; }
        [Required(ErrorMessage = "Contact information is required.")]
        public ICollection<SPRegResponseEntity> Responses { get; set; } = new List<SPRegResponseEntity>();

    }
}

using Core_Layer.Entities.Actors;
using Core_Layer.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Registeration_Request
{
    public class SPRegRequestEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "RequestDate is required.")]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public EnRegisterationRequestStatus Status { get; set; } 

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "RegistrationDocumentLink is required.")]
        [MaxLength(2083, ErrorMessage = "RegistrationDocumentLink cannot exceed 2083 characters.")]
        public  required string RegistrationDocumentLink { get; set; }

        // Foregin Keys
        [ForeignKey("ServiceProvider")]
        [Required(ErrorMessage = "ServiceProviderID is required.")]
        public int ServiceProviderID { get; set; }

        // Navigation Properties
        public ServiceProviderEntity? ServiceProvider { get; set; }
        public ICollection<SPRegResponseEntity> Responses { get; set; } = new List<SPRegResponseEntity>(); 

    }
}

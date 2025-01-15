using Core_Layer.Entities.Actors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request
{
    public class SPRegResponseEntity
    {
        [Key]
        public int ResponseID { get; set; }

        [Required(ErrorMessage = "ResponseText is required.")]
        [MaxLength(500, ErrorMessage = "ResponseText cannot exceed 500 characters.")]
        public string ResponseText { get; set; } = string.Empty;

        [Required(ErrorMessage = "ResponseDate is required.")]
        public DateTime ResponseDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Result is required.")]
        public bool Result { get; set; }

        // Foreign Keys
        [Required(ErrorMessage = "RequestID is required.")]
        [ForeignKey("Request")]
        public int RequestID { get; set; }
        [ForeignKey("RespondedBy")]
        public int? RespondedByID { get; set; }

        // Navigations
        [ForeignKey("RespondedByID")]
        public ManagerEntity? RespondedBy { get; set; }

        [ForeignKey("RequestID")]
        public SPRegRequestEntity Request { get; set; } = null!;
    }
}

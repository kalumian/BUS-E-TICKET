using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Actors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities.Actors.ServiceProvider;

namespace Core_Layer.DTOs
{
    public class SPRegResponseDTO
    {
        public int ResponseID { get; set; }
        public string ResponseText { get; set; } = string.Empty;
        public DateTime ResponseDate { get; set; } = DateTime.Now;
        public bool Result { get; set; }
        public int RequestID { get; set; }
        public string? RespondedByID { get; set; }
    }
}

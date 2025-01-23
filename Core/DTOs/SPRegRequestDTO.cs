using Core_Layer.Entities.Actors.ServiceProvider.Registeration_Request;
using Core_Layer.Entities.Actors.ServiceProvider;
using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.DTOs
{
    public class SPRegRequestDTO
    {
        public int SPRegRequestID { get; set; }
        public DateTime RequestDate { get; set; }
        public string? Notes { get; set; }
        public required BusinessDTO Business { get; set; }
        public required RegisterServiceProviderDTO ServiceProvider { get; set; }
        public SPRegResponseDTO? Response { get; set; }

    }
}

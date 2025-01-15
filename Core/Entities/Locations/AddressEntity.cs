using Core_Layer.Entities.Actors.ServiceProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Locations
{
    public class AddressEntity
    {
        [Key]
        public int AddressID { get; set; }

        [StringLength(250, ErrorMessage = "Additional details cannot exceed 250 characters.")]
        public string? AdditionalDetails { get; set; }

        [StringLength(700, ErrorMessage = "Location URL cannot exceed 700 characters.")]
        public string? LocationURL { get; set; }

        #region Foreign Keys

        [ForeignKey("Street")]
        public int? StreetID { get; set; }  // Optional to allow manual entry

        [ForeignKey("City")]
        [Required(ErrorMessage = "City ID is required.")]
        public int CityID { get; set; }

        #endregion

        #region Navigation Properties

        public StreetEntity? Street { get; set; }  // Optional navigation property
        public CityEntity? City { get; set; }
        public BusinessEntity? Business  { get; set; }


        #endregion
    }
}

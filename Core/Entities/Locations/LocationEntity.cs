using Core_Layer.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Locations
{
    public class LocationEntity
    {
        [Key]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "Location Name is required.")]
        [StringLength(100, ErrorMessage = "Location Name cannot exceed 100 characters.")]
        public required string LocationName { get; set; }


        [Required(ErrorMessage = "Map Location is required.")]
        [StringLength(250, ErrorMessage = "Map Location cannot exceed 250 characters.")]
        public required string MapLocation { get; set; }

        #region Foreign Keys
        [Required(ErrorMessage = "Address ID is required.")]
        [ForeignKey("Address")]
        public required int AddressID { get; set; }
        #endregion
        #region Navigation Properties
        public required AddressEntity Address { get; set; }
        public IEnumerable<LocationEntity>? StartLocation { get; set; }
        public IEnumerable<LocationEntity>? EndLocation { get; set; }
        #endregion
    }
}

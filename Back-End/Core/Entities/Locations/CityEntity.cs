using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Locations
{
    public class CityEntity
    {
        [Key]
        public int CityID { get; set; }

        [Required(ErrorMessage = "City Name is required.")]
        [StringLength(100, ErrorMessage = "City Name cannot exceed 100 characters.")]
        public required string CityName { get; set; }
        #region Foreign Keys

        [ForeignKey("Region")]
        [Required(ErrorMessage = "Region ID is required.")]
        public required int RegionID { get; set; }

        #endregion

        #region Navigation Properties

        public required RegionEntity Region { get; set; }

        public IEnumerable<StreetEntity>? Streets { get; set; }

        #endregion



    }
}

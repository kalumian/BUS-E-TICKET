using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Locations
{
    public class RegionEntity
    {
        [Key]
        public int RegionID { get; set; }

        [Required(ErrorMessage = "Region Name is required.")]
        [StringLength(100, ErrorMessage = "Region Name cannot exceed 100 characters.")]
        public string RegionName { get; set; }
        #region Foreign Keys

        [ForeignKey("Country")]
        [Required(ErrorMessage = "Country ID is required.")]
        public required int CountryID { get; set; }

        #endregion

        #region Navigation Properties

        public CountryEntity? Country { get; set; }

        public IEnumerable<CityEntity>? Cities { get; set; }

        #endregion

    }
}

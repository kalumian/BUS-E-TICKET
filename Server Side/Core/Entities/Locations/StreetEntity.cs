using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Locations
{
    public class StreetEntity
    {
        [Key]
        public int StreetID { get; set; }

        [Required(ErrorMessage = "Street Name is required.")]
        [StringLength(150, ErrorMessage = "Street Name cannot exceed 150 characters.")]
        public required string StreetName { get; set; }


        #region Foreign Keys

        [Required(ErrorMessage = "City ID is required.")]
        [ForeignKey("City")]
        public required int CityID { get; set; }

        #endregion

        #region Navigation Properties

        public required CityEntity City { get; set; }

        public IEnumerable<AddressEntity>? Address { get; set; }

        #endregion

    }
}

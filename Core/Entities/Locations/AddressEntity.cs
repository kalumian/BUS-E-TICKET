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

        #region Foreign Keys

        [ForeignKey("Street")]
        [Required(ErrorMessage = "Street ID is required.")]
        public required int StreetID { get; set; }

        #endregion

        #region Navigation Properties

        public required StreetEntity Street { get; set; }

        #endregion

    }
}

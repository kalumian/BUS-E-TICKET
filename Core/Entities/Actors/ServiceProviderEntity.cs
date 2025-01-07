using Core_Layer.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core_Layer.Interfaces.Actors_Interfaces;

namespace Core_Layer.Entities.Actors
{
    public class ServiceProviderEntity : IServiceProviderEntity
    {
        [Key]
        public int ServiceProviderID { get; set; }
        [MaxLength(100, ErrorMessage = "Logo URL cannot exceed 1000 characters.")]
        public required string BusinessName { get; set; }
        [Required(ErrorMessage = "Permission is LogoURL.")]
        [MaxLength(500, ErrorMessage = "Logo URL cannot exceed 1000 characters.")]
        public required string LogoURL { get; set; }

        #region Foreign Keys

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        [ForeignKey("ContactInformation")]
        public int ContactInformationID { get; set; }

        [ForeignKey("Account")]
        public required string AccountID { get; set; } 

        #endregion

        #region Navigation Properties

        [Required(ErrorMessage = "Address is required.")]
        public required AddressEntity Address { get; set; } 

        [Required(ErrorMessage = "Contact information is required.")]
        public required ContactInformationEntity ContactInformation { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        public required AuthoUser Account { get; set; }

        #endregion

    }
}

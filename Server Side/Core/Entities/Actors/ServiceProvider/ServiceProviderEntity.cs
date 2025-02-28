using Core_Layer.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core_Layer.Interfaces.Actors_Interfaces;
using Core_Layer.Enums;

namespace Core_Layer.Entities.Actors.ServiceProvider
{
    public class ServiceProviderEntity : IServiceProviderEntity
    {
        [Key]
        public int ServiceProviderID { get; set; }

        #region Foreign Keys
        [Required(ErrorMessage = "AccountID is required.")]
        [ForeignKey("Account")]
        public string? AccountID { get; set; }

        [Required(ErrorMessage = "AccountID is required.")]

        [ForeignKey("Business")]
        public int? BusinessID { get; set; }
        #endregion

        #region Navigation Properties
        public AuthoUser? Account { get; set; }
        public BusinessEntity? Business { get; set; }


        #endregion

    }
}

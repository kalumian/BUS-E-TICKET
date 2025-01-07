using Core_Layer.Entities.Locations;
using Core_Layer.Enums;
using Core_Layer.Interfaces.Actors_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.Entities.Actors
{
    public class CustomerEntity : ICustomerEntity
    {
        [Key]
        public int CustomerID { get; set; }

        #region Foreign Keys

        [ForeignKey("Person")]
        public int PersonID { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        [ForeignKey("ContactInformation")]
        public int ContactInformationID { get; set; }

        [ForeignKey("Account")]
        public required string AccountID { get; set; }

        #endregion

        #region Navigation Properties

        [Required(ErrorMessage = "Person is required.")]
        public required PersonEntity Person { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public required AddressEntity Address { get; set; }

        [Required(ErrorMessage = "Contact information is required.")]
        public required ContactInformationEntity ContactInformation { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        public required AuthoUser Account { get; set; }

        #endregion


    }
}

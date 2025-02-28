using Core_Layer.Entities.Locations;
using Core_Layer.Entities.Trip.Reservation;
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
    public class CustomerEntity
    {
        [Key]
        public int? CustomerID { get; set; }

        #region Foreign Keys
        [Required(ErrorMessage = "Person is required.")]
        [ForeignKey("Person")]
        public int? PersonID { get; set; }

        [ForeignKey("Address")]
        [Required(ErrorMessage = "Address is required.")]
        public int? AddressID { get; set; }

        [ForeignKey("Account")]
        [Required(ErrorMessage = "Account is required.")]
        public string? AccountID { get; set; }

        #endregion

        #region Navigation Properties

        public PersonEntity? Person { get; set; }
        public  AddressEntity? Address { get; set; }
        public  ReservationEntity? Reservations { get; set; }
        public AuthoUser? Account { get; set; }

        #endregion
    }
}

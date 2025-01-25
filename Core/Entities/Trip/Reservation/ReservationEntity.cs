using Core_Layer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities.Actors;
using Core_Layer.Entities.Trip;

namespace Core_Layer.Entities.Trip.Reservation
{
    public class ReservationEntity
    {
        #region Primary Properties

        [Key]
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "Reservation State is required.")]
        public EnRegisterationRequestStatus ReservationStatus { get; set; }

        [Required(ErrorMessage = "Seat Number is required.")]
        [Range(1, short.MaxValue, ErrorMessage = "Seat Number must be a positive number.")]
        public short SeatNumber { get; set; }

        [Required(ErrorMessage = "Reservation Date is required.")]
        public DateTime ReservationDate { get; set; }

        #endregion

        #region Foreign Keys

        [Required(ErrorMessage = "TripID is required.")]
        [ForeignKey("Trip")]
        public int TripID { get; set; }

        [Required(ErrorMessage = "PassengerID is required.")]
        [ForeignKey("Passenger")]
        public int PassengerID { get; set; }

        #endregion

        #region Navigation Properties

        public TripEntity? Trip { get; set; }

        public PassengerEntity? Passenger { get; set; }

        #endregion
    }
}

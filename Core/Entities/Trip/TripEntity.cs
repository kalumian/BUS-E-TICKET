using Core_Layer.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities.Actors;
using Core_Layer.Enums;

namespace Core_Layer.Entities.Trip
{
    public class TripEntity
    {
        [Key]
        public int TripID { get; set; } 

        [Required(ErrorMessage = "Vehicle Information is required.")]
        [StringLength(250, ErrorMessage = "Vehicle Information cannot exceed 250 characters.")]
        public string VehicleInfo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Driver Information is required.")]
        [StringLength(250, ErrorMessage = "Driver Information cannot exceed 250 characters.")]
        public string DriverInfo { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; } 

        [Required(ErrorMessage = "Total Seats is required.")]
        public short TotalSeats { get; set; } 

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; } 

        [Required(ErrorMessage = "Trip State is required.")]
        public EnTripStatus TripStatus { get; set; }

        public DateTime? EstimatedArrivalDate { get; set; }
        [Required(ErrorMessage = "TripDuration is required.")]
        public TimeSpan TripDuration { get; set; } 

        [Required(ErrorMessage = "Available Seats Count is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Available Seats Count cannot be negative.")]
        public short AvailableSeatsCount { get; set; }

        [Required(ErrorMessage = "Reserved Seats Binary is required.")]
        public required byte[] ReservedSeatsBinary { get; set; }

        #region Foreign Keys

        [Required(ErrorMessage = "ServiceProviderID is required.")]
        public int ServiceProviderID { get; set; }

        [ForeignKey("StartLocation")]
        [Required(ErrorMessage = "Start Location is required.")]
        public required int StartLocationID { get; set; }

        [ForeignKey("EndLocation")]
        [Required(ErrorMessage = "End Location is required.")]
        public required int EndLocationID { get; set; } 

        #endregion

        #region Navigation Properties

        [ForeignKey("ServiceProviderID")]
        public ServiceProviderEntity? ServiceProvider { get; set; } 

        public LocationEntity? StartLocation { get; set; } 

        public LocationEntity? EndLocation { get; set; } // العلاقة مع الكيان LocationEntity (نهاية الرحلة)

        #endregion

    }
}

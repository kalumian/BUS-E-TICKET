using Core_Layer.Entities.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core_Layer.Enums;
using Core_Layer.Entities.Actors.ServiceProvider;

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
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Start Date format.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid End Date format.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Total Seats is required.")]
        [Range(1, short.MaxValue, ErrorMessage = "Total Seats must be greater than 0.")]
        public short TotalSeats { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, float.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Trip State is required.")]
        public EnTripStatus TripStatus { get; set; }

        public DateTime? EstimatedArrivalDate { get; set; }

        [Required(ErrorMessage = "Trip Duration is required.")]
        [Range(typeof(TimeSpan), "00:01:00", "1.00:00:00", ErrorMessage = "Trip duration must be between 1 minute and 1 day.")]
        public TimeSpan TripDuration { get; set; }

        [Required(ErrorMessage = "Available Seats Count is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Available Seats Count cannot be negative.")]
        public short AvailableSeatsCount { get; set; }

        [Required(ErrorMessage = "Reserved Seats Binary is required.")]
        public required byte[] ReservedSeatsBinary { get; set; }

        #region Foreign Keys

        [ForeignKey("ServiceProvider")]
        [Required(ErrorMessage = "ServiceProviderID is required.")]
        public int ServiceProviderID { get; set; }

        [ForeignKey("StartLocation")]
        [Required(ErrorMessage = "Start Location is required.")]
        public required int StartLocationID { get; set; }

        [ForeignKey("EndLocation")]
        [Required(ErrorMessage = "End Location is required.")]
        public required int EndLocationID { get; set; }

        [ForeignKey("Currency")]
        [Required(ErrorMessage = "End CurrencyID is required.")]
        public required int CurrencyID { get; set; }

        #endregion

        #region Navigation Properties

        public ServiceProviderEntity? ServiceProvider { get; set; }
        public LocationEntity? StartLocation { get; set; }
        public LocationEntity? EndLocation { get; set; }
        public CurrencyEntity? Currency { get; set; }

        #endregion
    }
}

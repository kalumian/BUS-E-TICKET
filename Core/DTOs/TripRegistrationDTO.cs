using Core_Layer.Enums;
public class TripRegistrationDTO
{
    public string VehicleInfo { get; set; } = string.Empty;
    public string DriverInfo { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public short TotalSeats { get; set; }
    public decimal Price { get; set; }
    public EnTripStatus TripStatus { get; set; }
    public DateTime? EstimatedArrivalDate { get; set; }
    public TimeSpan TripDuration { get; set; }
    public int ServiceProviderID { get; set; } 
    public LocationDTO StartLocation { get; set; } = new LocationDTO();
    public LocationDTO EndLocation { get; set; } = new LocationDTO();
}

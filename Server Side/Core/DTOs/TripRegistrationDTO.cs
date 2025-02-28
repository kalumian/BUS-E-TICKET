using Core_Layer.Enums;
public class TripRegistrationDTO
{
    public int? TripID { get; set; }
    public string? VehicleInfo { get; set; } = string.Empty;
    public string? DriverInfo { get; set; } = string.Empty;
    public DateTime? TripDate { get; set; }
    public short? TotalSeats { get; set; }
    public decimal? Price { get; set; }   
    public EnTripStatus? TripStatus { get; set; }

    public int? TripDuration { get; set; }
    public string ServiceProviderID { get; set; }
    public int? CurrencyID { get; set; }
    public LocationDTO? StartLocation { get; set; } = new LocationDTO();
    public LocationDTO? EndLocation { get; set; } = new LocationDTO();
}

public class TripDisplayDTO
{
    public int TripID { get; set; }
    public string VehicleInfo { get; set; } = string.Empty;
    public string DriverInfo { get; set; } = string.Empty;
    public DateTime TripDate { get; set; }
    public DateTime ArrivalDate { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public TimeSpan TripDuration { get; set; }
    public short TotalSeats { get; set; }
    public short BookedSeatCount { get; set; }
    public string TripStatus { get; set; } = string.Empty;

    public string BusinessName { get; set; } = string.Empty;
    public string BusinessLogoURL { get; set; } = string.Empty;

    public string StartLocationName { get; set; } = string.Empty;
    public string StartLocationURL { get; set; } = string.Empty;
    public string StartCity { get; set; } = string.Empty;
    public string StartAdditionalDetails { get; set; } = string.Empty;

    public string EndLocationName { get; set; } = string.Empty;
    public string EndLocationURL { get; set; } = string.Empty;
    public string EndCity { get; set; } = string.Empty;
    public string EndAdditionalDetails { get; set; } = string.Empty;
}

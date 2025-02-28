using Core_Layer.DTOs;

public class LocationDTO
{
    public int? LocationID { get; set; }
    public string? LocationName { get; set; } = string.Empty;
    public string? LocationURL { get; set; } = string.Empty;
    public AddressDTO? Address { get; set; } = new AddressDTO();
}
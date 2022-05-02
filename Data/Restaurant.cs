namespace RestaurantWebAPI.Data;

/// <summary>
/// Restaurant.
/// </summary>
public class Restaurant
{
    public Int32 Id { get; set; }
    public String Name { get; set; } = String.Empty;
    public String Country { get; set; } = String.Empty;
    public String KitchenType { get; set; } = String.Empty;
    public Double Rating { get; set; }
    public Double Latitude { get; set; }
    public Double Longitude { get; set; }
}
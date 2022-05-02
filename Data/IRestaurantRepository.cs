namespace RestaurantWebAPI.Data;

/// <summary>
/// The restaurant's repository.
/// </summary>
public interface IRestaurantRepository : IDisposable
{
    /// <summary>
    /// Getting all the restaurants.
    /// </summary>
    Task<List<Restaurant>> GetRestaurantsAsync();
    
    /// <summary>
    /// Getting restaurants by name.
    /// </summary>
    /// <param name="name"> Name. </param>
    Task<List<Restaurant>> GetRestaurantsAsync(String name);
    
    /// <summary>
    /// Getting restaurants by coordinates.
    /// </summary>
    /// <param name="coordinate"> Latitude and longitude. </param>
    Task<List<Restaurant>> GetRestaurantsAsync(Coordinate coordinate);
    
    /// <summary>
    /// Getting restaurants by ID.
    /// </summary>
    /// <param name="restaurantId"> Identifier. </param>
    Task<Restaurant?> GetRestaurantAsync(Int32 restaurantId);
    
    /// <summary>
    /// Adding a restaurant.
    /// </summary>
    /// <param name="restaurant"> Restaurant. </param>
    Task InsertRestaurantAsync(Restaurant restaurant);
    
    /// <summary>
    /// Updating an existing restaurant.
    /// </summary>
    /// <param name="restaurant"> Restaurant. </param>
    Task UpdateRestaurantAsync(Restaurant restaurant);
    
    /// <summary>
    /// Deleting a restaurant.
    /// </summary>
    /// <param name="restaurantId"> Identifier. </param>
    Task DeleteRestaurantAsync(Int32 restaurantId);
    
    /// <summary>
    /// Saving.
    /// </summary>
    Task SaveAsync();
}
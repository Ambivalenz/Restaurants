namespace RestaurantWebAPI.Data;

public sealed class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantDb _context;

    public RestaurantRepository(RestaurantDb context)
    {
        _context = context;
    }

    public Task<List<Restaurant>> GetRestaurantsAsync() => 
        _context.Restaurants.ToListAsync();

    public Task<List<Restaurant>> GetRestaurantsAsync(String name) => 
        _context.Restaurants.Where(r => r.Name.Contains(name)).ToListAsync();

    public async Task<List<Restaurant>> GetRestaurantsAsync(Coordinate coordinate) =>
        await _context.Restaurants.Where(restaurant =>
                restaurant.Latitude > coordinate.Latitude - 1 &&
                restaurant.Latitude < coordinate.Latitude + 1 &&
                restaurant.Longitude > coordinate.Longitude - 1 &&
                restaurant.Longitude < coordinate.Longitude + 1)
            .ToListAsync();

    public async Task<Restaurant?> GetRestaurantAsync(Int32 restaurantId) => 
        await _context.Restaurants.FindAsync(new object?[] {restaurantId});

    public async Task InsertRestaurantAsync(Restaurant restaurant) => 
        await _context.Restaurants.AddAsync(restaurant);

    public async Task UpdateRestaurantAsync(Restaurant restaurant)
    {
        Restaurant? restaurantFromDb =
            await _context.Restaurants.FindAsync(new object?[] {restaurant.Id});
        
        if (restaurantFromDb == null)
        {
            return;
        }

        restaurantFromDb.Name = restaurant.Name;
        restaurantFromDb.Country = restaurant.Country;
        restaurantFromDb.KitchenType = restaurant.KitchenType;
        restaurantFromDb.Rating = restaurant.Rating;
        restaurantFromDb.Longitude = restaurant.Longitude;
        restaurantFromDb.Latitude = restaurant.Latitude;
    }

    public async Task DeleteRestaurantAsync(Int32 restaurantId)
    {
        Restaurant? restaurantFromDb =
            await _context.Restaurants.FindAsync(new object?[] {restaurantId});
        
        if (restaurantFromDb == null)
        {
            return;
        }

        _context.Restaurants.Remove(restaurantFromDb);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    private bool _disposed;
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
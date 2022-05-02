namespace RestaurantWebAPI.Data;

/// <summary>
/// Restaurant database.
/// </summary>
public class RestaurantDb : DbContext
{
    public RestaurantDb(DbContextOptions<RestaurantDb> options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
}
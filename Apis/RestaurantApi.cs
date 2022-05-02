namespace RestaurantWebAPI.Apis;

/// <summary>
/// Interaction with the API of the restaurant.
/// </summary>
public class RestaurantApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/restaurants", Get)
            .Produces<List<Restaurant>>(StatusCodes.Status200OK)
            .WithName("GetAllRestaurants")
            .WithTags("Getters");

        app.MapGet("/restaurants/{id}", GetById)
            .Produces<List<Restaurant>>(StatusCodes.Status200OK)
            .WithName("GetRestaurant")
            .WithTags("Getters");

        app.MapPost("/restaurants", Post)
            .Accepts<Restaurant>("application/json")
            .Produces<List<Restaurant>>(StatusCodes.Status201Created)
            .WithName("CreateRestaurant")
            .WithTags("Creators");

        app.MapPut("/restaurants", Put)
            .Accepts<Restaurant>("application/json")
            .WithName("UpdateRestaurant")
            .WithTags("Updaters");

        app.MapDelete("restaurant/{id}", Delete)
            .WithName("DeleteRestaurant")
            .WithTags("Deleters");

        app.MapGet("/restaurants/search/name/{query}",
                SearchByName)
            .Produces<List<Restaurant>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("SearchRestaurants")
            .WithTags("Getters")
            .ExcludeFromDescription();

        app.MapGet("/restaurants/search/location/{coordinate}",
                SearchByCoordinates)
            .ExcludeFromDescription();
    }

    [Authorize]
    private async Task<IResult> Get(IRestaurantRepository repository) =>
        Results.Extensions.Xml(await repository.GetRestaurantsAsync());
    
    [Authorize]
    private async Task<IResult> GetById(Int32 id, IRestaurantRepository repository) =>
        await repository.GetRestaurantAsync(id) is Restaurant restaurant
            ? Results.Ok(restaurant)
            : Results.NotFound();

    [Authorize]
    private async Task<IResult> Post([FromBody] Restaurant restaurant, IRestaurantRepository repository)
    {
        await repository.InsertRestaurantAsync(restaurant);
        await repository.SaveAsync();
        return Results.Created($"/restaurants/{restaurant.Id}", restaurant);
    }

    [Authorize]
    private async Task<IResult> Put([FromBody] Restaurant restaurant, IRestaurantRepository repository)
    {
        await repository.UpdateRestaurantAsync(restaurant);
        await repository.SaveAsync();
        return Results.NoContent();
    }

    [Authorize]
    private async Task<IResult> Delete(Int32 id, IRestaurantRepository repository)
    {
        await repository.DeleteRestaurantAsync(id);
        await repository.SaveAsync();
        return Results.NoContent();
    }

    [Authorize]
    private async Task<IResult> SearchByName(String query, IRestaurantRepository repository) =>
        await repository.GetRestaurantsAsync(query) is IEnumerable<Restaurant> restaurants
            ? Results.Ok(restaurants)
            : Results.NotFound(Array.Empty<Restaurant>());

    [Authorize]
    private async Task<IResult> SearchByCoordinates(Coordinate coordinate, IRestaurantRepository repository) =>
        await repository.GetRestaurantsAsync(coordinate) is IEnumerable<Restaurant> restaurants
            ? Results.Ok(restaurants)
            : Results.NotFound(Array.Empty<Restaurant>());
}
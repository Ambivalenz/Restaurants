namespace RestaurantWebAPI.Apis;

/// <summary>
/// Api.
/// </summary>
public interface IApi
{
    /// <summary>
    /// Registration.
    /// </summary>
    /// <param name="app"> Application. </param>
    void Register(WebApplication app);
}
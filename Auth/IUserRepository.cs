namespace RestaurantWebAPI.Auth;

/// <summary>
/// User repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Getting the user.
    /// </summary>
    /// <param name="userModel"> UserName and Password. </param>
    /// <returns> User. </returns>
    UserDto GetUser(UserModel userModel);
}
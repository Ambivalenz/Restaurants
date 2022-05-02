namespace RestaurantWebAPI.Auth;

/// <summary>
/// Implementation of the restaurant repository.
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// List of users.
    /// </summary>
    private static List<UserDto> Users => new()
    {
        new UserDto("John", "123"),
        new UserDto("Alex", "123"),
        new UserDto("Nancy", "123")
    };

    /// <summary>
    /// Getting the user.
    /// </summary>
    /// <param name="userModel"> User. </param>
    /// <returns> The found account. </returns>
    /// <exception cref="Exception"> The user was not found. </exception>
    public UserDto GetUser(UserModel userModel) =>
        Users.FirstOrDefault(user =>
            String.Equals(user.UserName, userModel.UserName) &&
            String.Equals(user.Password, userModel.Password)) ??
        throw new Exception("User not found!");
}
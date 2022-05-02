namespace RestaurantWebAPI.Auth;

/// <summary>
/// User information.
/// </summary>
/// <param name="UserName"> UserName. </param>
/// <param name="Password"> Password. </param>
public record UserDto(String UserName, String Password);

/// <summary>
/// Custom model.
/// </summary>
public record UserModel
{
    /// <summary>
    /// User login.
    /// </summary>
    [Required] public string UserName { get; set; } = String.Empty;
    
    /// <summary>
    /// The user's password.
    /// </summary>
    [Required] public string Password { get; set; } = String.Empty;
}
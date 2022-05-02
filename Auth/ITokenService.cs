namespace RestaurantWebAPI.Auth;

/// <summary>
/// Service for working with the token.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Creating a token. 
    /// </summary>
    /// <param name="key"> Key. </param>
    /// <param name="issuer"> Issuer. </param>
    /// <param name="user"> User. </param>
    /// <returns> New token. </returns>
    String BuildToken(String key, String issuer, UserDto user);
}
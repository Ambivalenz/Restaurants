namespace RestaurantWebAPI.Auth;

/// <summary>
/// Service for creating an access key.
/// </summary>
public class TokenService : ITokenService
{
    /// <summary>
    /// The lifetime of the token.
    /// </summary>
    private readonly TimeSpan _expiryDuration = new TimeSpan(0, 30, 0);
    
    /// <summary>
    /// Creating a token.
    /// </summary>
    /// <param name="key"> Key. </param>
    /// <param name="issuer"> Issuer. </param>
    /// <param name="user"> User. </param>
    /// <returns> New token.</returns>
    public string BuildToken(string key, string issuer, UserDto user)
    {
        Claim[] claims = {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };
        
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        
        SigningCredentials credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature);
        
        JwtSecurityToken tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            expires: DateTime.Now.Add(_expiryDuration), signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
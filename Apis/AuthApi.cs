namespace RestaurantWebAPI.Apis;

/// <summary>
/// Authorization API.
/// </summary>
public class AuthApi : IApi
{
    /// <summary>
    /// Register.
    /// </summary>
    /// <param name="app"> Application. </param>
    public void Register(WebApplication app)
    {
        app.MapGet("/login", [AllowAnonymous] async (HttpContext context,
            ITokenService tokenService, IUserRepository userRepository) =>
        {
            UserModel userModel = new()
            {
                UserName = context.Request.Query["username"]!,
                Password = context.Request.Query["password"]!
            };
            UserDto? userDto = userRepository.GetUser(userModel);
            
            if (userDto == null)
            {
                return Results.Unauthorized();
            }

            String token = tokenService.BuildToken(app.Configuration["Jwt:Key"]!,
                app.Configuration["Jwt:Issuer"]!, userDto);
            return Results.Ok(token);
        });
    }
}
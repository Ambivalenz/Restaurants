WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

WebApplication app = builder.Build();

Configure(app);

IEnumerable<IApi> apis = app.Services.GetServices<IApi>();

foreach (IApi? api in apis)
{
    if (api is null)
    {
        throw new InvalidProgramException("Api not found!");
    }

    api.Register(app);
}

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddDbContext<RestaurantDb>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")!);
    });
    services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    services.AddSingleton<ITokenService>(new TokenService());
    services.AddSingleton<IUserRepository>(new UserRepository());
    services.AddAuthorization();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

    services.AddTransient<IApi, RestaurantApi>();
    services.AddTransient<IApi, AuthApi>();
}

void Configure(WebApplication app)
{
    app.UseAuthentication();
    app.UseAuthorization();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        using IServiceScope scope = app.Services.CreateScope();
        RestaurantDb db = scope.ServiceProvider.GetRequiredService<RestaurantDb>();
        db.Database.EnsureCreated();
    }

    app.UseHttpsRedirection();
}
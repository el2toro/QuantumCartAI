using Auth.API.Models;
using Auth.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

// JWT Authentication
var jwtKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddAuthorization();

const string corsPolicy = "luxe-eccomerce-policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()     // needed if sending cookies or Authorization headers
            .SetIsOriginAllowed(origin => true); // allow all origins (unsafe for prod, adjust below)
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(corsPolicy);
app.UseAuthentication();
app.UseAuthorization();
// Seed users
app.Services.GetRequiredService<IUserService>().Seed();

// Login endpoint
app.MapPost("/auth/login", (LoginRequest req,
                            IUserService users,
                            ITokenService tokens) =>
{
    var user = users.ValidateUser(req.Username, req.Password);
    if (user is null)
        return Results.Unauthorized();

    var access = tokens.GenerateAccessToken(user);
    var refresh = tokens.GenerateRefreshToken();

    return Results.Ok(new AuthResponse(access, refresh));
});

// Protected test endpoint
app.MapGet("/auth/me", (ClaimsPrincipal user) =>
{
    var username = user.Identity!.Name;
    return Results.Ok(new { username });
}).RequireAuthorization();

app.Run();

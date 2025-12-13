using Auth.API.Services;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

//builder.Services.AddAuthorization();
//builder.Services.AddAuthorization();

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

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(corsPolicy);
//app.UseAuthentication();
//app.UseAuthorization();
// Seed users
app.Services.GetRequiredService<IUserService>().Seed();


app.MapCarter();

app.Run();

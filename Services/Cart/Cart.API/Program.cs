using MediatR;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "CartService";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();


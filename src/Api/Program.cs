using Application.NoSpecification;
using Domain.Core;
using Domain.Passengers.DataAccess;
using Domain.Passengers.Services;
using Domain.Pessengers;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
var conStr = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<AirContext>(options =>
    options.UseNpgsql(conStr), ServiceLifetime.Transient);


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<Program>();
    config.RegisterServicesFromAssemblyContaining<GetPassengerFlightNumNoSpecificationHandler>();
});

builder.Services.AddScoped<IPassengerSpecificationProvider, PassengerSpecificationProvider>();
builder.Services.AddScoped<IPassengerNoSpecificationProvider, PassengerNoSpecificationProvider>();

builder.Services.AddScoped<IPassengerNoSpecificationService, PassengerNoSpecificationService>();
builder.Services.AddScoped<IPassengerSpecificationService, PassengerSpecificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

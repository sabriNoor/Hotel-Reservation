using FluentValidation.AspNetCore;
using HotelReservation.API.Middlewares;
using HotelReservation.Application;
using HotelReservation.Infrastructure;
using HotelReservation.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = Environments.Development
});


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; 
});
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();

// Infrastructure DI
builder.Services.AddInfrastructure(builder.Configuration);

// Application DI
builder.Services.AddApplicationServices();

var app = builder.Build();
await app.Services.MigrateAndSeedDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseExceptionHandling();

app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

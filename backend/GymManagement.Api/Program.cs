using System.Text.Json.Serialization;
using GymManagement.Api.Entities;
using GymManagement.Api.Infrastructure;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Repositories;
using GymManagement.Api.Services;
using GymManagement.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args); // Prepare the blueprint and gather the building materials

// Read the "MongoDbSettings" section from appsettings.json and bind it to the MongoDbSettings class
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Register MongoClient as a Singleton — created ONCE and reused for the whole app lifetime
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings!.ConnectionString);
});

builder.Services.AddOpenApi(); // Use OpenAPI provided by the ASP.NET Core framework (part of .NET)

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); // Enable controllers via .AddControllers() (routing), and globally convert enums to strings via .AddJsonOptions()

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddHostedService<MongoIndexInitializer>();

builder.Services.AddScoped<IRepository<Equipment>>(sp =>
    new MongoRepository<Equipment>(
        sp.GetRequiredService<IMongoClient>(),
        sp.GetRequiredService<IOptions<MongoDbSettings>>(),
        "equipments"));

var app = builder.Build(); // Use all the prepared "materials" to build the app — the actual running web application

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Only used in the dev environment; skipped in production so it won't show up
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection(); // Automatically redirect users from insecure HTTP links to secure HTTPS links

app.UseAuthorization(); // Turn on permission checking

app.MapControllers(); // Goes together with AddControllers() above: that one sets things up, this one activates it by wiring the request URLs to the matching controller methods

app.Run(); // Run
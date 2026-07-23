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

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings!.ConnectionString);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddHostedService<MongoIndexInitializer>();

builder.Services.AddScoped<IRepository<Equipment>>(sp =>
    new MongoRepository<Equipment>(
        sp.GetRequiredService<IMongoClient>(),
        sp.GetRequiredService<IOptions<MongoDbSettings>>(),
        "equipments"));

builder.Services.AddScoped<IRepository<Member>>(sp =>
new MongoRepository<Member>(
sp.GetRequiredService<IMongoClient>(),
sp.GetRequiredService<IOptions<MongoDbSettings>>(),
"members"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
     {
         options.RouteTemplate = "openapi/{documentName}.json";
     });
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Gym Management API")
               .WithTheme(ScalarTheme.Kepler) 
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
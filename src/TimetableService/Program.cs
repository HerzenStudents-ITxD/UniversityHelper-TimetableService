// Program.cs — точка входа для TimetableService
// .NET 9, современный стиль, все DI и Swagger подключены

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using UniversityHelper.TimetableService.Business.Interfaces;
using UniversityHelper.TimetableService.Business.Services;
using UniversityHelper.TimetableService.Data;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using StackExchange.Redis;
using RabbitMQ.Client;
using Serilog;
using System.Text.Json.Serialization;
using System;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Добавляем Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

// Добавляем контроллеры (API endpoints)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Swagger — для тестирования и документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Timetable API", 
        Version = "v1",
        Description = "API для серверного модуля расписания виртуального гида Герценовского университета",
        Contact = new OpenApiContact
        {
            Name = "UniversityHelper Team",
            Email = "support@universityhelper.ru"
        }
    });
});

// Подключение к базе данных через строку подключения из appsettings.json
string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<TimetableDbContext>(options =>
    options.UseNpgsql(connectionString));

// Добавляем Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["ConnectionStrings:Redis"];
    options.InstanceName = builder.Configuration["Cache:Redis:InstanceName"];
});

// Добавляем RabbitMQ
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri(builder.Configuration["ConnectionStrings:RabbitMQ"])
    };
    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
});

// Add caching
builder.Services.AddMemoryCache();

// Add validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString)
    .AddRedis(builder.Configuration["ConnectionStrings:Redis"])
    .AddRabbitMQ(async sp => 
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(builder.Configuration["ConnectionStrings:RabbitMQ"] ?? "amqp://localhost")
        };
        return await factory.CreateConnectionAsync();
    });

// DI: сервис для работы с расписанием
builder.Services.AddScoped<ITimetableService, TimetableService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// Health checks
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

IHost appHost = app;
app.Run();

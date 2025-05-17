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
using UniversityHelper.TimetableService.Data.Provider;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры (API endpoints)
builder.Services.AddControllers();

// Swagger — для тестирования и документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timetable API", Version = "v1" });
});

// Подключение к базе данных через строку подключения из appsettings.json
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<TimetableDbContext>(options =>
    options.UseSqlite(connectionString));

// DI: сервис для работы с расписанием
builder.Services.AddScoped<ITimetableService, TimetableService>();

WebApplication app = builder.Build();

// Swagger только в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // минимальный стиль, без параметров
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Запуск приложения
app.Run();

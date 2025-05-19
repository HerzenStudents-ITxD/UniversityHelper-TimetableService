# University Helper - Timetable Service v2

Сервис для управления расписанием университета.

## Функционал
- Управление группами
- Управление предметами
- Управление изменениями расписания
- Кэширование данных
- Валидация и логирование

## Технологии
- .NET 9
- Entity Framework Core 9.0.4
- FluentValidation
- xUnit
- Swagger
- PostgreSQL
- Redis
- RabbitMQ

## Установка
```bash
git clone https://github.com/yourusername/UniversityHelper-TimetableService.git
cd UniversityHelper-TimetableService
dotnet restore
dotnet ef database update
dotnet run
```

## API
Swagger UI: `https://localhost:7001/swagger`

## Тесты
```bash
dotnet test
```

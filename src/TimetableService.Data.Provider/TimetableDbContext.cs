using Microsoft.EntityFrameworkCore;
using UniversityHelper.TimetableService.Data.Models;

namespace UniversityHelper.TimetableService.Data.Provider;

public class TimetableDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    public TimetableDbContext(DbContextOptions<TimetableDbContext> options)
        : base(options)
    {
    }

    // Конструктор без параметров для поддержки миграций EF Core
    public TimetableDbContext() { }

    // Добавляю OnConfiguring для поддержки миграций EF Core
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=universityhelper.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Конфигурации убраны, если нужны - перенести сюда из Data.Models
    }
} 
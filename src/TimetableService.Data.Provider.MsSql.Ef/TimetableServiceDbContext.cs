using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.TimetableService.Models.Db;

namespace UniversityHelper.TimetableService.Data.Provider.MsSql.Ef;

public class TimetableServiceDbContext : DbContext, IDataProvider
{
  public DbSet<DbGroup> Groups { get; set; }
  public DbSet<DbSubject> Subjects { get; set; }

  public TimetableServiceDbContext(DbContextOptions<TimetableServiceDbContext> options)
    : base(options)
  {
  }

  // Fluent API is written here.
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("UniversityHelper.TimetableService.Models.Db"));
  }

  public object MakeEntityDetached(object obj)
  {
    Entry(obj).State = EntityState.Detached;
    return Entry(obj).State;
  }

  async Task IBaseDataProvider.SaveAsync()
  {
    await SaveChangesAsync();
  }

  void IBaseDataProvider.Save()
  {
    SaveChanges();
  }

  public void EnsureDeleted()
  {
    Database.EnsureDeleted();
  }

  public bool IsInMemory()
  {
    return Database.IsInMemory();
  }
}

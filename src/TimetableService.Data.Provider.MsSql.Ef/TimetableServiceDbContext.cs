﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.TimetableService.Data.Models;
using UniversityHelper.TimetableService.Data.Provider;

namespace UniversityHelper.TimetableService.Data.Provider.MsSql.Ef;

public class TimetableServiceDbContext : DbContext, IDataProvider
{
  public DbSet<Group> Groups { get; set; }
  public DbSet<Subject> Subjects { get; set; }
  public DbSet<TimetableChange> TimetableChanges { get; set; }

  public TimetableServiceDbContext(DbContextOptions<TimetableServiceDbContext> options)
    : base(options)
  {
  }

  // Fluent API is written here.
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    // Здесь можно добавить дополнительные настройки моделей, если необходимо
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

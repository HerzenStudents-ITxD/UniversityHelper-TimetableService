using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.TimetableService.Models.Db;
public class DbSubject
{
  public const string TableName = "Subjects";
  public Guid Id { get; set; }
  public Guid GroupId { get; set; }
  public DbGroup Group { get; set; }
  public string Name { get; set; }
  public DateTime Date { get; set; }
  public string Professor { get; set; }
  public Guid? PointId { get; set; }
  public DateTime UpdateAt { get; set; }
  public string Place { get; set; }
}
public class DbSubjectConfiguration : IEntityTypeConfiguration<DbSubject>
{
  public void Configure(EntityTypeBuilder<DbSubject> builder)
  { 
    builder
      .ToTable(DbSubject.TableName);

    builder
      .HasKey(x => x.Id);

    builder
      .HasOne(ua => ua.Group)
      .WithMany(u => u.Subjects);

  }
}

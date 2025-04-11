using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace UniversityHelper.TimetableService.Models.Db;

public class DbGroup
{
  public const string TableName = "Groups";
  public Guid Id { get; set; }
  public string Institute { get; set; }
  public string Faculcity { get; set; }
  public string Degree { get; set; }
  public string FormEducation { get; set; }
  public int Course { get; set; }
  public string Group { get; set; }
  public string Direction { get; set; }
  public string SubGroup { get; set; }
  public DateTime UpdateAt { get; set; }
  [IgnoreParse]
  public ICollection<DbSubject> Subjects { get; set; }
  public DbGroup ()
  {
    Subjects = new HashSet<DbSubject> ();
  }

}
public class DbGroupConfiguration : IEntityTypeConfiguration<DbGroup>
{
  public void Configure(EntityTypeBuilder<DbGroup> builder)
  {
    builder
      .ToTable(DbGroup.TableName);

    builder.
      HasKey(p => p.Id);
    builder
      .HasMany(u => u.Subjects)
      .WithOne(uc => uc.Group);
  }
}

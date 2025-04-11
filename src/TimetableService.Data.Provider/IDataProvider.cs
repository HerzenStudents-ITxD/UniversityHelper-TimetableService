using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.Core.Enums;
using UniversityHelper.TimetableService.Models.Db;

namespace UniversityHelper.TimetableService.Data.Provider;

[AutoInject(InjectType.Scoped)]
public interface IDataProvider : IBaseDataProvider
{
  DbSet<DbGroup> Groups { get; set; }
  DbSet<DbSubject> Subjects { get; set; }

}

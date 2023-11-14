using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.TimetableService.Models.Dto.Requests.Models;

public class DailyTimetable
{
  public Guid GroupId;
  public DateTime WhenLoaded;
  public List<TimetableSubject> Subjects;
}

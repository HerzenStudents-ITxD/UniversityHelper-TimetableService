using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.TimetableService.Models.Dto.Requests.Models;

public class TimetableSubject
{
  public DateTime StartTime;
  public DateTime EndTime;
  public string SubjectFullName;
  public string SubjectName;
  public string Teacher;
  public string Address;
  public string Classroom;
  public string Comment;
}

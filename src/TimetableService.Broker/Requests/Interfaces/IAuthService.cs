using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Models.Broker.Responses.Auth;

namespace UniversityHelper.TimetableService.Broker.Requests.Interfaces;

[AutoInject]
public interface IAuthService
{
  Task<IGetTokenResponse> GetTokenAsync(Guid userId, List<string> errors);
}

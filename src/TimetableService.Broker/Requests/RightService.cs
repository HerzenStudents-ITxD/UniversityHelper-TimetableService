﻿using UniversityHelper.Core.BrokerSupport.Helpers;
using UniversityHelper.Models.Broker.Models.Right;
using UniversityHelper.Models.Broker.Requests.Rights;
using UniversityHelper.Models.Broker.Responses.Rights;
using UniversityHelper.TimetableService.Broker.Requests.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;

namespace UniversityHelper.TimetableService.Broker.Requests;

public class RightService : IRightService
{
  private readonly IRequestClient<IGetUserRolesRequest> _rcGetUserRoles;
  private readonly ILogger<RightService> _logger;

  public RightService(
    IRequestClient<IGetUserRolesRequest> rcGetUserRoles,
    ILogger<RightService> logger)
  {
    _rcGetUserRoles = rcGetUserRoles;
    _logger = logger;
  }

  public async Task<List<RoleData>> GetRolesAsync(
    Guid userId,
    string locale,
    List<string> errors,
    CancellationToken cancellationToken = default)
  {
    //TO DO add cache
    return (await RequestHandler.ProcessRequest<IGetUserRolesRequest, IGetUserRolesResponse>(
        _rcGetUserRoles,
        IGetUserRolesRequest.CreateObj(userIds: new() { userId }, locale: locale),
        errors,
        _logger))
      ?.Roles;
  }
}

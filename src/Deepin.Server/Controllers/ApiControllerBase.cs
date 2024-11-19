﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public abstract class ApiControllerBase : ControllerBase
{
}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Hola.Health.AuditLoggingService.Controllers;

[Route("api/audit-logging/demo")]
public class DemoController : AbpController
{
    [HttpGet]
    [Route("hello")]
    public async Task<string> HelloWorld()
    {
        return await Task.FromResult("Hello World!");
    }
    
    [HttpGet]
    [Route("hello-authorized")]
    [Authorize]
    public async Task<string> HelloWorldAuthorized()
    {
        return await Task.FromResult("Hello World (Authorized)!");
    }
}
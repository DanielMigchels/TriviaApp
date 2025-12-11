using Microsoft.AspNetCore.Mvc;

namespace TriviaApp.API.Controllers;

public class TriviaControllerBase : ControllerBase
{
    protected string RemoteIpAddress => HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
}

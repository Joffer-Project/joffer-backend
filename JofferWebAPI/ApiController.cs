using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JofferWebAPI;

// Controllers/ApiController.cs

[Route("api")]
public class ApiController : Controller
{
    [HttpGet("private-scoped")]
    [Authorize("read:messages")]
    public IActionResult Scoped()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated and have a scope of read:messages to see this."
        });
    }

    [HttpGet("private")]
    [Authorize]
    public IActionResult Private()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint! You need to be authenticated to see this."
        });
    }
}

// Controllers/ApiController.cs



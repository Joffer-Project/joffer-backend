using Microsoft.AspNetCore.Mvc;

namespace JofferWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class JobOfferController : ControllerBase
{
    private static readonly string[] JobOffers = new[]
    {
        "IT consultant", "Java developer", "C# developer"
    };

    private readonly ILogger<JobOfferController> _logger;

    public JobOfferController(ILogger<JobOfferController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetAll")]
    public IEnumerable<string> Get()
    {
        return JobOffers;
    }
}


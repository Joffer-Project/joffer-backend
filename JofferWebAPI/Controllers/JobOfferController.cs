using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace JofferWebAPI.Controllers;

[Route("api")]
[ApiController]
public class JobOfferController : ControllerBase
{
    private static readonly string[] JobOffers = new[]
    {
        "IT consultant", "Java developer", "C# developer"
    };

    private readonly ILogger<JobOfferController> _logger;
    private IConfiguration _configuration;

    public JobOfferController(ILogger<JobOfferController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    //[HttpGet("private")]
    //[Authorize]
    //public IActionResult Private()
    //{
    //    return Ok(new
    //    {
    //        Message = "Hello from a private endpoint! You need to be authenticated to see this."
    //    });
    //}

    //[HttpGet("private-scoped")]
    //[Authorize("read:messages")]
    //public IActionResult Scoped()
    //{
    //    return Ok(new
    //    {
    //        Message = "Hello from a private endpoint! You need to be authenticated and have a scope of read:messages to see this."
    //    });
    //}

    [Authorize]
    [HttpGet("GetAll")]
    public IEnumerable<string> Get()
    {
        string cs = _configuration.GetConnectionString("DefaultConnection");
        List<string> accountNames = new List<string>();
        string statement = "SELECT * FROM accounts";
        var con = new MySqlConnection(cs);
        con.Open();
        var cmd = new MySqlCommand(statement, con);
        MySqlDataReader reader = cmd.ExecuteReader();
        
        int name = reader.GetOrdinal("name");

        while (reader.Read())
        {
            string accountName = reader.IsDBNull(name) ? null : reader.GetString(name);
            accountNames.Add(accountName);
        }
        con.Close();
        
        return accountNames;
    }
}
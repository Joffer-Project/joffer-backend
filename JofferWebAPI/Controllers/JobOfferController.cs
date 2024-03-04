using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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
    private IConfiguration _configuration;

    public JobOfferController(ILogger<JobOfferController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet("GetAll")]
    [Authorize]
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
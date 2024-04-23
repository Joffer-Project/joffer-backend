using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JofferWebAPI.Context;
using JofferWebAPI.Dtos;
using JofferWebAPI.Models;

namespace JofferWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DbContextRender _context;

        public AccountController(DbContextRender context)
        {
            _context = context;
        }
        
        // GET: api/Account
        [ServiceFilter(typeof(AuthActionFilter))]
        [HttpGet("Account")]
        
        public async Task<ActionResult<Account>> GetAccount()
        {
            var account = HttpContext.Items["UserAccount"] as Account;
            // Use the account information as needed
            return Ok(account);
        }

        // GET: api/Account
        [HttpGet("Accounts/GetAll")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            // return await _context.Accounts.ToListAsync();
            return await _context.Accounts.Select(x => new AccountDto(x)).ToListAsync();
        }
    }
}
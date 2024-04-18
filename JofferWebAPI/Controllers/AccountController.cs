using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JofferWebAPI.Context;
using JofferWebAPI.Dtos;
using JofferWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace JofferWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    [ServiceFilter(typeof(AuthActionFilter))]
    public class AccountController : ControllerBase
    {
        private readonly DbContextRender _context;

        public AccountController(DbContextRender context)
        {
            _context = context;
        }
        
        // GET: api/Account
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

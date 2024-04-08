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
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }
            
            return account;
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

        // GET: api/Account/5
        [HttpGet("Account/{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
    }
}

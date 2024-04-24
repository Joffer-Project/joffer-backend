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

            var accounts = await _context.Accounts
                .Select(x => new AccountDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Auth0Id = x.Auth0Id,
                    Password = x.Password,
                    AccountType = x.AccountType,
                    Email = x.Email,
                    // Set PhoneNumber to "no number" if it's null
                    PhoneNumber = x.PhoneNumber ?? "no number",
                    IsPremium = x.IsPremium,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return accounts;
        }

    }
}
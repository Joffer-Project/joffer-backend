using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JofferWebAPI.Context;
using JofferWebAPI.Models;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DbContextRender _context;

        public CompanyController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet("OnlyFORDEBUGGING")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
          if (_context.Companies == null)
          {
              return NotFound();
          }
            return await _context.Companies.ToListAsync();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
          if (_context.Companies == null)
          {
              return NotFound();
          }
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }
        
        // POST: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TalentDto>> PostCompany(CompanyDto companyDto)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'MyDbContext.Companies'  is null.");
            }
          
            AccountDto accountDto = new AccountDto();
            accountDto.Name = companyDto.Name;
            accountDto.Auth0Id = companyDto.Auth0Id;
            accountDto.Email = companyDto.Email;
            accountDto.IsPremium = companyDto.IsPremium;
            accountDto.IsActive = true;
            accountDto.AccountType = "Company";
            accountDto.Password = "";
          
            _context.Accounts.Add(new Account(accountDto));
          
            await _context.SaveChangesAsync();
          
            var recentlyCreatedAccount = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == accountDto.Auth0Id);

            if (recentlyCreatedAccount == null)
            {
                return Problem("Failed to retrieve recently created account.");
            }

            companyDto.AccountId = recentlyCreatedAccount.Id;
            _context.Companies.Add(new Company(companyDto));
          
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = companyDto.Id }, companyDto);
        }
    }
}

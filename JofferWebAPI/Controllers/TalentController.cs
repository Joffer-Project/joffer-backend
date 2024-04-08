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
using Microsoft.Build.Framework;

namespace JofferWebAPI.Controllers
{
    [Route("[controller]" + "s")]
    [ApiController]
    public class TalentController : ControllerBase
    {
        private readonly DbContextRender _context;

        public TalentController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Talent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Talent>>> GetTalents()
        {
          if (_context.Talents == null)
          {
              return NotFound();
          }
          return await _context.Talents
              .Where(t => t.IsActive == true)
              .ToListAsync();
        }

        // GET: api/Talent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Talent>> GetTalent(int id)
        {
          if (_context.Talents == null)
          {
              return NotFound();
          }
          var applicant = await _context.Talents.FindAsync(id);

          if (applicant == null)
          {
              return NotFound();
          }

          return applicant;
        }

        // POST: api/Talent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TalentDto>> PostTalent(TalentDto talentDto)
        {
          if (_context.Talents == null)
          {
              return Problem("Entity set 'MyDbContext.Talents'  is null.");
          }
          
          AccountDto accountDto = new AccountDto();
          accountDto.Name = talentDto.Name;
          accountDto.Auth0Id = talentDto.Auth0Id;
          accountDto.Email = talentDto.Email;
          accountDto.IsPremium = talentDto.IsPremium;
          accountDto.IsActive = true;
          accountDto.AccountType = "Applicant";
          accountDto.Password = "";
          
          _context.Accounts.Add(new Account(accountDto));
          
          await _context.SaveChangesAsync();
          
          var recentlyCreatedAccount = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == accountDto.Auth0Id);

          if (recentlyCreatedAccount == null)
          {
              return Problem("Failed to retrieve recently created account.");
          }

          talentDto.AccountId = recentlyCreatedAccount.Id;
          _context.Talents.Add(new Talent(talentDto));
          
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetTalent", new { id = talentDto.Id }, talentDto);
        }
        
        // PUT: api/Talent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TalentDto>> PutTalent(int id, TalentDto talentDto)
        {
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.Id == id);

            if (talent == null)
            {
                return Problem($"Talent with id {id} not found. (Do not enter the accountId, but the talentId instead.");
            }
            
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == talent.AccountId);
            
            if (account == null)
            {
                return Problem("Account not found!");
            }

            talent.AboutMe = talentDto.AboutMe;
            talent.EmploymentStatus = talentDto.EmploymentStatus;
            talent.SalaryMinimum = talentDto.SalaryMinimum;
            talent.AvatarUrl = talentDto.AvatarUrl;
            talent.Image2Url = talentDto.Image2Url;
            talent.Image3Url = talentDto.Image3Url;
            talent.Image4Url = talentDto.Image4Url;
            talent.Image5Url = talentDto.Image5Url;
            talent.DribbleUrl = talentDto.DribbleUrl;
            talent.LinkedInUrl = talentDto.LinkedInUrl;
            talent.MediumUrl = talentDto.MediumUrl;
            talent.PersonalUrl = talentDto.PersonalUrl;
            talent.GitHubUrl = talentDto.GitHubUrl;
            talent.IsActive = talentDto.IsActive;
            
            _context.Entry(talent).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Talents.FirstOrDefaultAsync(t=>t.Id == id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            account.Auth0Id = talentDto.Auth0Id;
            account.Email = talentDto.Email;
            account.Password = "NO PASSWORD";
            account.Name = talentDto.Name;
            account.AccountType = "Applicant";
            account.IsPremium = talentDto.IsPremium;
            account.IsActive = talentDto.IsActive;
            
            _context.Entry(account).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Accounts.FirstOrDefaultAsync(a=>a.Id == talent.AccountId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTalent(int id)
        {
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.Id == id);

            if (talent == null)
            {
                return Problem("Talent does not exist.");
            }
            
            talent.IsActive = false;
            
            _context.Entry(talent).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == talent.AccountId);
            
            if (account == null)
            {
                return Problem("Account connected to the talent does not exist.");
            }

            account.IsActive = false;
            
            _context.Entry(account).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}

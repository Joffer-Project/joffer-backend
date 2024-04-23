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
    [Route("")]
    [ApiController]
    public class TalentController : ControllerBase
    {
        private readonly DbContextRender _context;

        public TalentController(DbContextRender context)
        {
            _context = context;
        }
        
        // GET: api/Talent
        [HttpGet("Talents")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<TalentWithJobOfferId>>> GetTalents()
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }
            
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);

            if (company == null)
            {
                return Problem($"Company not found. (No company bindend to the account with Auth0Id {account.Auth0Id}. Make sure to activate this controller with a company account.)");
            }
            
            if (_context.Talents == null)
            {
                return NotFound();
            }

            var companyJobOffers =  _context.JobOffers.Where(jo => jo.CompanyId == company.Id);

            var jobOfferSwipesTalentInterested = _context.JobOfferSwipes
                .Where(jos => jos.TalentInterested == true)
                .Where(jos => jos.FinalMatch == false)
                .Where(jos => jos.IsActive == true)
                .Join(companyJobOffers,
                    jos => jos.JobOfferId,
                    jo => jo.Id,
                    (jos, jo) => jos)
                .ToList();

            var talentsInterested = _context.Talents
                .ToList()
                .Join(jobOfferSwipesTalentInterested,
                    talent => talent.Id,
                    jos => jos.TalentId,
                    (talent, josti) => new { TalentWithJobOfferId = talent, JobOfferId = josti.JobOfferId })
                        .ToList();

            return Ok(talentsInterested);
        }

        // GET: api/Talent/5
        [HttpGet("Talent")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<TalentDto>> GetTalent()
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(a => a.AccountId == account.Id);
            
            if (talent == null)
            {
                return Problem("Not a talent!");
            }

            return new TalentDto(talent);
        }

        // POST: api/Talent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Talent")]
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

            var newTalent = new Talent(talentDto);
            _context.Talents.Add(newTalent);
            await _context.SaveChangesAsync();

            talentDto.Id = newTalent.Id;


            return CreatedAtAction("GetTalent", new { id = talentDto.Id }, talentDto);
        }
        
        // PUT: api/Talent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Talent")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<TalentDto>> PutTalent(TalentDto talentDto)
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == account.Id);

            if (talent == null)
            {
                return Problem($"Talent not found. (No talent bindend to the account.)");
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
                if (talent == null)
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
                if (account == null)
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
        
        [HttpDelete("Talent/{id}")]
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
        
        [HttpGet("/Talents/GetAll")]
        public async Task<ActionResult<IEnumerable<Talent>>> GetAllTalents()
        {
            if (_context.Talents == null)
            {
                return NotFound();
            }
            return await _context.Talents.ToListAsync();
        }
    }
}

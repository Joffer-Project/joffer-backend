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
          return await _context.Talents.ToListAsync();
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

        // PUT: api/Talent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTalent(int id, TalentDto applicantDto)
        {
            Talent applicant = new(applicantDto)
            {
                Id = id,
            };

            if (id != applicant.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalentExists(id))
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

        // DELETE: api/Talent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTalent(int id)
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

            _context.Talents.Remove(applicant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TalentExists(int id)
        {
            return (_context.Talents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

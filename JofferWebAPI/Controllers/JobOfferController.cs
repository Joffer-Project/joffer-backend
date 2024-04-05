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
    [Route("JobOffers")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly DbContextRender _context;

        public JobOfferController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/JobOffers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetJobOffers()
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found.");
            }

            string userSub = userSubClaim.Value;

            // Retrieve the AccountID associated with the user's Auth0Id
            int accountId = await GetAccountIdByAuth0Id(userSub);

            if (accountId == 0)
            {
                // No matching account found
                return NotFound("Account not found.");
            }

            // Query the JobOffers table based on the CompanyID associated with the retrieved AccountID
            var jobOffers = await _context.JobOffers
                                    .Where(j => j.Company.AccountId == accountId)
                                    .ToListAsync();

            return jobOffers;
        }

        // GET: api/JobOffers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobOffer>> GetJobOffer(int id)
        {
          if (_context.JobOffers == null)
          {
              return NotFound();
          }
            var jobOffer = await _context.JobOffers.FindAsync(id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            return jobOffer;
        }

        // PUT: api/JobOffer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobOffer(int id, JobOfferDto jobOfferDto)
        {
            JobOffer jobOffer = new(jobOfferDto)
            {
                Id = id,
            };

            if (id != jobOffer.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobOfferExists(id))
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

        // POST: api/JobOffer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobOfferDto>> PostJobOffer(JobOfferDto jobOfferDto)
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found.");
            }

            string userSub = userSubClaim.Value;

            // Retrieve the AccountID associated with the user's Auth0Id
            int accountId = await GetAccountIdByAuth0Id(userSub);

            if (accountId == 0)
            {
                // No matching account found
                return NotFound("Account not found.");
            }

            // Retrieve the CompanyID associated with the retrieved AccountID
            int companyId = await GetCompanyIdByAccountId(accountId);

            if (companyId == 0)
            {
                // No matching company found
                return NotFound("Company not found.");
            }

            // Set the CompanyID for the job offer
            jobOfferDto.CompanyId = companyId;

            // Add the job offer to the context
            _context.JobOffers.Add(new JobOffer(jobOfferDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobOffer", new { id = jobOfferDto.Id }, jobOfferDto);
        }


        // DELETE: api/JobOffer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobOffer(int id)
        {
            if (_context.JobOffers == null)
            {
                return NotFound();
            }
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<int> GetAccountIdByAuth0Id(string auth0Id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Auth0Id == auth0Id);
            return account?.Id ?? 0;
        }

        private async Task<int> GetCompanyIdByAccountId(int accountId)
        {
            var company = await _context.Companies
                                        .Where(c => c.AccountId == accountId)
                                        .FirstOrDefaultAsync();

            return company?.Id ?? 0;
        }

        private bool JobOfferExists(int id)
        {
            return (_context.JobOffers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

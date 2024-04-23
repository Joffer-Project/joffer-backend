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

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetAll()
        {
            if (_context.JobOffers == null)
            {
                return NotFound();
            }
            return await _context.JobOffers.ToListAsync();
        }


        // GET: api/JobOffers
        [HttpGet("Company")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetJobOffersCompany()
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            // Query the JobOffers table based on the CompanyID associated with the retrieved AccountID
            var jobOffers = await _context.JobOffers
                                    .Where(j => j.Company.AccountId == account.Id)
                                    .ToListAsync();

            return jobOffers;
        }

        [HttpGet("Talent")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetJobOffersTalent()
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == account.Id);
            if (talent == null) return BadRequest("User is not a talent.");
            
            var jobOfferSwipes = await _context.JobOfferSwipes
                .Where(jos=>jos.TalentId == talent.Id)
                .Select(jos => jos.JobOfferId)
                .ToListAsync();
            
            var jobOffers = await _context.JobOffers
                .Where(j=>!jobOfferSwipes.Contains(j.Id))
                .ToListAsync();

            return jobOffers;
        }

        // GET: api/JobOffers/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<JobOffer>> GetJobOffer(int id)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var jobOffer = await _context.JobOffers
                .FirstOrDefaultAsync(j => j.Company.AccountId == account.Id && j.Id == id);
            if (jobOffer == null) return NotFound("Job offer not found.");
            

            return jobOffer;
        }

        // PUT: api/JobOffer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<IActionResult> PutJobOffer(int id, JobOfferDto jobOfferDto)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var jobOffer = await _context.JobOffers
                .Include(j => j.Company)
                .FirstOrDefaultAsync(j => j.Id == id && j.Company.AccountId == account.Id);
            if (jobOffer == null) return NotFound("Job offer not found.");

            jobOffer.Title = jobOfferDto.Title;
            jobOffer.Description = jobOfferDto.Description;
            jobOffer.MinSalary = jobOfferDto.MinSalary;
            jobOffer.MaxSalary = jobOfferDto.MaxSalary;
            jobOffer.EmploymentStatus = jobOfferDto.EmploymentStatus;
            jobOffer.CompanyId = jobOfferDto.CompanyId;
            jobOffer.IsActive = jobOfferDto.IsActive;

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
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<JobOfferDto>> PostJobOffer(JobOfferDto jobOfferDto)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            int companyId = await GetCompanyIdByAccountId(account.Id);
            if (companyId == 0) return NotFound("Company not found.");
            

            jobOfferDto.CompanyId = companyId;

            var newJobOffer = new JobOffer(jobOfferDto);
            _context.JobOffers.Add(newJobOffer);
            await _context.SaveChangesAsync();

            jobOfferDto.Id = newJobOffer.Id;

            return CreatedAtAction("GetJobOffer", new { id = jobOfferDto.Id }, jobOfferDto);
        }



        // DELETE: api/JobOffer/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<IActionResult> DeleteJobOffer(int id)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            if (_context.JobOffers == null) return NotFound();
            

            var jobOffer = await _context.JobOffers
                .Include(j => j.Company)
                .FirstOrDefaultAsync(j => j.Id == id && j.Company.AccountId == account.Id);

            if (jobOffer == null) return NotFound();

            jobOffer.IsActive = false;

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

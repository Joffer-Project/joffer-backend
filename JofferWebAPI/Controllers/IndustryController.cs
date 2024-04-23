using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JofferWebAPI.Context;
using JofferWebAPI.Models;

namespace JofferWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly DbContextRender _context;

        public IndustryController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Industry
        [HttpGet("/Industries/Account")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Industry>>> GetAccountIndustries()
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var accountIndustries = _context.AccountIndustries
                .Where(ar => ar.AccountId == account.Id)
                .Where(ar => ar.IsActive == true)
                .ToList();

            var industries = accountIndustries
                .Join(_context.Industry,
                    ar => ar.IndustryId,
                    r => r.Id,
                    (ar, r) => r)
                .ToList();

            return industries;
        }

        [HttpGet("/Industries/JobOffer/{jobofferId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Industry>>> GetJobOfferIndustries(int jobofferId)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobofferId);
            if (jobOffer == null) return Problem($"Job offer with id {jobofferId} not found!");


            var jobOfferIndustries = _context.JobOfferIndustries
                .Where(jor => jor.JobOfferId == jobofferId)
                .Where(jor => jor.IsActive == true)
                .ToList();

            var industries = jobOfferIndustries
                .Join(_context.Industry,
                    jor => jor.IndustryId,
                    r => r.Id,
                    (jor, r) => r)
                .ToList();

            return industries;
        }

        // GET: api/Industry
        [HttpGet("/Industries/GetAll")]
        public async Task<ActionResult<IEnumerable<Industry>>> GetAllIndustries()
        {
            if (_context.Industry == null)
            {
                return NotFound();
            }
            return await _context.Industry.ToListAsync();
        }

        // POST: api/Industry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Industry")]
        public async Task<ActionResult<Industry>> PostIndustry(Industry industry)
        {
            if (_context.Industry == null) return Problem("Entity set 'DbContextRender.Industry'  is null.");
            
            _context.Industry.Add(industry);
            await _context.SaveChangesAsync();

            return Ok("Posted.");
        }

        [HttpPost("Industry/Account/{industryId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<Industry>> PostIndustryToAccount(int industryId)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var industry = await _context.Industry.FirstOrDefaultAsync(r => r.Id == industryId);
            if (industry == null) return Problem($"Industry with id {industryId} not found!");


            AccountIndustries accountIndustries = new()
            {
                AccountId = account.Id,
                IndustryId = industryId,
                IsActive = true
            };

            _context.AccountIndustries.Add(accountIndustries);
            await _context.SaveChangesAsync();

            return Ok("Success!");
        }

        [HttpPost("Industry/{industryId}/JobOffer/{jobOfferId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<Industry>> PostIndustryToJobOffer(int industryId, int jobOfferId)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var industry = await _context.Industry.FirstOrDefaultAsync(r => r.Id == industryId);
            if (industry == null) return Problem($"Industry with id {industryId} not found!");


            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);
            if (company == null) return Problem($"Account with id {account.Id} is not a company!");
            

            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId);
            if (jobOffer == null) return Problem($"Job offer with id {jobOfferId} not found!");

            if (jobOffer.CompanyId != company.Id) return Problem($"Job offer with id {jobOfferId} does not belong to this company!");


            JobOfferIndustries jobOfferIndustries = new()
            {
                JobOfferId = jobOfferId,
                IndustryId = industryId,
                IsActive = true
            };

            _context.JobOfferIndustries.Add(jobOfferIndustries);
            await _context.SaveChangesAsync();

            return Ok("Success!");
        }

        [HttpDelete("Industry/{industryId}/JobOffer/{jobOfferId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<IActionResult> DeleteIndustryFromAccount(int industryId, int jobOfferId)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var industry = await _context.Industry.FirstOrDefaultAsync(r => r.Id == industryId);
            if (industry == null) return Problem($"Industry with id {industryId} not found!");


            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);
            if (company == null) return Problem($"Account with id {account.Id} is not a company!");
            

            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId);
            if (jobOffer == null) return Problem($"Job offer with id {jobOfferId} not found!");

            if (jobOffer.CompanyId != company.Id) return Problem($"Job offer with id {jobOfferId} does not belong to this company!");


            var jobOfferIndustries = _context.JobOfferIndustries
                .Where(ar => ar.JobOfferId == jobOfferId)
                .Where(ar => ar.IndustryId == industryId)
                .ToList();

            if (jobOfferIndustries.Count == 0) return Problem("Joboffer industry does not exists.");
            
            // is this neccessary?
            foreach (var jobOfferIndustry in jobOfferIndustries)
            {
                _context.JobOfferIndustries.Remove(jobOfferIndustry);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpDelete("Industry/Account/{industryId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<IActionResult> DeleteIndustryFromAccount(int industryId)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var accountIndustries = _context.AccountIndustries
                .Where(ar => ar.AccountId == account.Id)
                .Where(ar => ar.IndustryId == industryId)
                .ToList();

            if (accountIndustries.Count == 0) return Problem("Account industry(s) does not exists.");
            
            // is this really neccessary?
            foreach (var accountIndustry in accountIndustries)
            {
                _context.AccountIndustries.Remove(accountIndustry);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}

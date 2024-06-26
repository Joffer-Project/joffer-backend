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
    public class ActionController : ControllerBase
    {
        private readonly DbContextRender _context;

        public ActionController(DbContextRender context)
        {
            _context = context;
        }
        
        [HttpPost("/Like/{jobOfferId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<JobOfferSwipe>>> LikeCompany(int jobOfferId)
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == account.Id);

            if (talent == null)
            {
                return Problem($"Logged in user with auth0Id: {account.Auth0Id} is not a talent.");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(j => j.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id: {jobOfferId} does not exists.");
            }
            
            var existingJobOfferSwipe = await _context.JobOfferSwipes.FirstOrDefaultAsync(js => js.TalentId == talent.Id && js.JobOfferId == jobOfferId);
            
            if (existingJobOfferSwipe != null)
            {
                return Problem($"Job offer with id {jobOfferId} already swipe by talent with auth0id {account.Auth0Id}");
            }
            
            JobOfferSwipe jobOfferSwipe = new JobOfferSwipe();
            jobOfferSwipe.JobOfferId = jobOfferId;
            jobOfferSwipe.TalentId = talent.Id;
            jobOfferSwipe.TalentInterested = true;
            jobOfferSwipe.IsActive = true;
            jobOfferSwipe.FinalMatch = false;
            
            _context.JobOfferSwipes.Add(jobOfferSwipe);
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // PUT: Like/{jobOfferId}/Talent/{auth0Id}
        [HttpPut("/Like/{jobOfferId}/Talent/{auth0Id}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Talent>>> LikeTalent(string auth0Id, int jobOfferId)
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);

            if (company == null)
            {
                return Problem($"Logged in user with auth0Id: {account.Auth0Id} is not a company.");
            }
            
            var talentAccount = await _context.Accounts.FirstOrDefaultAsync(ta => ta.Auth0Id == auth0Id);

            if (talentAccount == null)
            {
                return Problem($"TalentAccount with Auth0Id: {auth0Id} does not exists.");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == talentAccount.Id);

            if (talent == null)
            {
                return Problem($"Talent with Talentid: {talentAccount.Id} does not exists.");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(j => j.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id: {jobOfferId} does not exists.");
            }

            if (jobOffer.CompanyId != company.Id)
            {
                Problem("This is not a job offer from this company.");
            }

            var jobOfferSwipe = await _context.JobOfferSwipes
                .Where(js => js.TalentId == talent.Id)
                .Where(js => js.JobOfferId == jobOfferId)
                .FirstOrDefaultAsync();

            if (jobOfferSwipe == null)
            {
                return Problem(
                    $"Job offer swipe does not exists. In other words, the talent (auth0Id: {auth0Id}) has not swiped the job offer with id: {jobOfferId}");
            }

            jobOfferSwipe.FinalMatch = true;
            
            _context.Entry(jobOfferSwipe).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPost("/Dislike/{jobOfferId}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<JobOfferSwipe>>> DislikeCompany(int jobOfferId)
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == account.Id);

            if (talent == null)
            {
                return Problem($"Logged in user with auth0Id: {account.Auth0Id} is not a talent.");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(j => j.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id: {jobOfferId} does not exists.");
            }
            
            var existingJobOfferSwipe = await _context.JobOfferSwipes.FirstOrDefaultAsync(js => js.TalentId == talent.Id && js.JobOfferId == jobOfferId);

            if (existingJobOfferSwipe != null)
            {
                return Problem($"Job offer with id {jobOfferId} already swipe by talent with auth0id {account.Auth0Id}");
            }
            
            JobOfferSwipe jobOfferSwipe = new JobOfferSwipe();
            jobOfferSwipe.JobOfferId = jobOfferId;
            jobOfferSwipe.TalentId = talent.Id;
            jobOfferSwipe.TalentInterested = false;
            jobOfferSwipe.IsActive = true;
            jobOfferSwipe.FinalMatch = false;
            
            _context.JobOfferSwipes.Add(jobOfferSwipe);
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
        [HttpPut("/Dislike/{jobOfferId}/Talent/{auth0Id}")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<Talent>>> DislikeTalent(string auth0Id, int jobOfferId)
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);

            if (company == null)
            {
                return Problem($"Logged in user with auth0Id: {account.Auth0Id} is not a company.");
            }
                        
            var talentAccount = await _context.Accounts.FirstOrDefaultAsync(ta => ta.Auth0Id == auth0Id);

            if (talentAccount == null)
            {
                return Problem($"TalentAccount with Auth0Id: {auth0Id} does not exists.");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == talentAccount.Id);

            if (talent == null)
            {
                return Problem($"Talent with Talentid: {talentAccount.Id} does not exists.");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(j => j.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id: {jobOfferId} does not exists.");
            }

            if (jobOffer.CompanyId != company.Id)
            {
                Problem("This is not a job offer from this company.");
            }

            var jobOfferSwipe = await _context.JobOfferSwipes
                .Where(js => js.TalentId == talent.Id)
                .Where(js => js.JobOfferId == jobOfferId)
                .FirstOrDefaultAsync();

            if (jobOfferSwipe == null)
            {
                return Problem(
                    $"Job offer swipe does not exists. In other words, the talent (auth0Id: {auth0Id}) has not swiped the job offer with id: {jobOfferId}");
            }

            jobOfferSwipe.TalentInterested = false;
            jobOfferSwipe.FinalMatch = false;
            
            _context.Entry(jobOfferSwipe).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("/Matches")]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<IEnumerable<JobOfferSwipe>>> GetAllAccounts()
        {
            var account = HttpContext.Items["UserAccount"] as Account;

            if (account == null)
            {
                return Problem("Failed to fetch the account");
            }
            
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == account.Id);

            if (company != null)
            {
                var jobOfferSwipes = await _context.JobOfferSwipes
                    .Where(jos=>jos.FinalMatch == true)
                    .Join(
                        _context.JobOffers,
                        jos => jos.JobOfferId,
                        jo => jo.Id,
                        (jos, jo) => new { JobOfferSwipe = jos, JobOffer = jo }
                    )
                    .Where(joined => joined.JobOffer.CompanyId == company.Id)
                    .Select(joined => joined.JobOfferSwipe)
                    .ToListAsync();

                return jobOfferSwipes;
            }

            if (talent != null)
            {
                var jobOfferSwipes = await _context.JobOfferSwipes
                    .Where(jos => jos.TalentId == talent.Id)
                    .Where(jos => jos.FinalMatch == true)
                    .ToListAsync();

                return jobOfferSwipes;
            }
            
            return BadRequest("User is not a talent and is not a company.");
        }
    }
}

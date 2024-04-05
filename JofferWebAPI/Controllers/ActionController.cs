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
        public async Task<ActionResult<IEnumerable<JobOfferSwipe>>> LikeCompany(int jobOfferId)
        {
            string loggedInUserSub = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var loggedInUser = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == loggedInUserSub);

            if (loggedInUser == null)
            {
                return Problem($"Logged in user with auth0Id: {loggedInUserSub} does not exists.");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == loggedInUser.Id);

            if (talent == null)
            {
                return Problem($"Logged in user with auth0Id: {loggedInUserSub} is not a talent.");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(j => j.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id: {jobOfferId} does not exists.");
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
        public async Task<ActionResult<IEnumerable<Talent>>> LikeTalent(string auth0Id, int jobOfferId)
        {
            string loggedInUserSub = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var loggedInUser = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == loggedInUserSub);

            if (loggedInUser == null)
            {
                return Problem($"Logged in user with auth0Id: {auth0Id} does not exists.");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == loggedInUser.Id);

            if (company == null)
            {
                return Problem($"Logged in user with auth0Id: {auth0Id} is not a company.");
            }
            
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Auth0Id == auth0Id);
            
            if (account == null)
            {
                return Problem($"Account with auth0Id: {auth0Id} does not exists.");
            }
            
            var talent = await _context.Talents.FirstOrDefaultAsync(t => t.AccountId == account.Id);

            if (talent == null)
            {
                return Problem($"Talent with accountId: {account.Id} does not exists.");
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
    }
}

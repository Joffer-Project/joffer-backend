using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JofferWebAPI.Context;
using JofferWebAPI.Models;

namespace JofferWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DbContextRender _context;

        public RoleController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet("/Roles/Account")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAccountRoles()
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }

            var accountRoles = _context.AccountRoles
                .Where(ar => ar.AccountId == account.Id)
                .Where(ar => ar.IsActive == true)
                .ToList();
            
            var roles = accountRoles
                .Join(_context.Roles,
                    ar => ar.RoleId,
                    r => r.Id,
                    (ar, r) => r) 
                .ToList();
            
            return roles;
        }
        
        [HttpGet("/Roles/JobOffer/{jobofferId}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetJobOfferRoles(int jobofferId)
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }

            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobofferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id {jobofferId} not found!");
            }

            var jobOfferRoles = _context.JobOfferRoles
                .Where(jor => jor.JobOfferId == jobofferId)
                .Where(jor => jor.IsActive == true)
                .ToList();
            
            var roles = jobOfferRoles
                .Join(_context.Roles,
                    jor => jor.RoleId,
                    r => r.Id,
                    (jor, r) => r) 
                .ToList();
            
            return roles;
        }
        
        // GET: api/Role
        [HttpGet("/Roles/GetAll")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            return await _context.Role.ToListAsync();
        }

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Role")]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
          if (_context.Role == null)
          {
              return Problem("Entity set 'DbContextRender.Role'  is null.");
          }
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return Ok("Posted.");
        }
        
        [HttpPost("Role/Account/{roleId}")]
        public async Task<ActionResult<Role>> PostRoleToAccount(int roleId)
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }
            
            var role = _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return Problem($"Role with id {roleId} not found!");
            }
            
            AccountRoles accountRoles = new AccountRoles();
            accountRoles.AccountId = account.Id;
            accountRoles.RoleId = roleId;
            accountRoles.IsActive = true;
            
            _context.AccountRoles.Add(accountRoles);
            await _context.SaveChangesAsync();

            return Ok("Success!");
        }
        
        [HttpPost("Role/{roleId}/JobOffer/{jobOfferId}")]
        public async Task<ActionResult<Role>> PostRoleToJobOffer(int roleId, int jobOfferId)
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }
            
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return Problem($"Role with id {roleId} not found!");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);
            
            if (company == null)
            {
                return Problem($"Account with id {account.Id} is not a company!");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id {jobOfferId} not found!");
            }

            if (jobOffer.CompanyId != company.Id)
            {
                return Problem($"Job offer with id {jobOfferId} does not belong to this company!");
            }
            
            JobOfferRoles jobOfferRoles = new JobOfferRoles();
            jobOfferRoles.JobOfferId = jobOfferId;
            jobOfferRoles.RoleId = roleId;
            jobOfferRoles.IsActive = true;
            
            _context.JobOfferRoles.Add(jobOfferRoles);
            await _context.SaveChangesAsync();

            return Ok("Success!");
        }
        
        [HttpDelete("Role/{roleId}/JobOffer/{jobOfferId}")]
        public async Task<IActionResult> DeleteRoleFromAccount(int roleId, int jobOfferId)
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }
            
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return Problem($"Role with id {roleId} not found!");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AccountId == account.Id);
            
            if (company == null)
            {
                return Problem($"Account with id {account.Id} is not a company!");
            }
            
            var jobOffer = await _context.JobOffers.FirstOrDefaultAsync(jo => jo.Id == jobOfferId);

            if (jobOffer == null)
            {
                return Problem($"Job offer with id {jobOfferId} not found!");
            }

            if (jobOffer.CompanyId != company.Id)
            {
                return Problem($"Job offer with id {jobOfferId} does not belong to this company!");
            }
            
            var jobOfferRoles = _context.JobOfferRoles
                .Where(ar => ar.JobOfferId == jobOfferId)
                .Where(ar => ar.RoleId == roleId)
                .ToList();

            if (jobOfferRoles.Count == 0)
            {
                return Problem("Joboffer role(s) does not exists.");
            }

            foreach (var jobOfferRole in jobOfferRoles)
            {
                _context.JobOfferRoles.Remove(jobOfferRole);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
        
        [HttpDelete("Role/Account/{roleId}")]
        public async Task<IActionResult> DeleteRoleFromAccount(int roleId)
        {
            var userSubClaim = User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                // User is not authenticated or user identifier claim is not found
                return BadRequest("User identifier claim not found. (In other words, user is not logged in)");
            }

            string userSub = userSubClaim.Value;
            
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);
            
            if (account == null)
            {
                return Problem($"Account with Auth0Id {userSub} not found!");
            }
            
            var accountRoles = _context.AccountRoles
                .Where(ar => ar.AccountId == account.Id)
                .Where(ar => ar.RoleId == roleId)
                .ToList();

            if (accountRoles.Count == 0)
            {
                return Problem("Account role(s) does not exists.");
            }

            foreach (var accountRole in accountRoles)
            {
                _context.AccountRoles.Remove(accountRole);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return (_context.Role?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

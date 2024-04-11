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
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DbContextRender _context;

        public RoleController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
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
        
        // GET: api/Role
        [HttpGet("GetAll")]
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
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
          if (_context.Role == null)
          {
              return Problem("Entity set 'DbContextRender.Role'  is null.");
          }
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }
        
        [HttpPost("{roleId}")]
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
        
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRoleFromAccount(int roleId)
        {
            //TODO: nog fixen als er twee rows zijn met dezelfde account- en roleid.
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
            
            AccountRoles accountRole = _context.AccountRoles.FirstOrDefault(ar => ar.AccountId == account.Id && ar.RoleId == roleId);

            if (accountRole == null)
            {
                return Problem("Account role does not exists.");
            }
            
            accountRole.IsActive = false;
            _context.Entry(accountRole).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return (_context.Role?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

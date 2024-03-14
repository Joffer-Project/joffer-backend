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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDiciplineController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AccountDiciplineController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountDicipline
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDicipline>>> GetAccountDiciplines()
        {
          if (_context.AccountDiciplines == null)
          {
              return NotFound();
          }
            return await _context.AccountDiciplines.ToListAsync();
        }

        // GET: api/AccountDicipline/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDicipline>> GetAccountDicipline(int id)
        {
          if (_context.AccountDiciplines == null)
          {
              return NotFound();
          }
            var accountDicipline = await _context.AccountDiciplines.FindAsync(id);

            if (accountDicipline == null)
            {
                return NotFound();
            }

            return accountDicipline;
        }

        // PUT: api/AccountDicipline/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountDicipline(int id, AccountDicipline accountDicipline)
        {
            if (id != accountDicipline.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(accountDicipline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountDiciplineExists(id))
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

        // POST: api/AccountDicipline
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDicipline>> PostAccountDicipline(AccountDicipline accountDicipline)
        {
          if (_context.AccountDiciplines == null)
          {
              return Problem("Entity set 'MyDbContext.AccountDiciplines'  is null.");
          }
            _context.AccountDiciplines.Add(accountDicipline);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountDiciplineExists(accountDicipline.AccountId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountDicipline", new { id = accountDicipline.AccountId }, accountDicipline);
        }

        // DELETE: api/AccountDicipline/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDicipline(int id)
        {
            if (_context.AccountDiciplines == null)
            {
                return NotFound();
            }
            var accountDicipline = await _context.AccountDiciplines.FindAsync(id);
            if (accountDicipline == null)
            {
                return NotFound();
            }

            _context.AccountDiciplines.Remove(accountDicipline);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountDiciplineExists(int id)
        {
            return (_context.AccountDiciplines?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
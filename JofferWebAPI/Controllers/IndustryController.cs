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
    public class IndustryController : ControllerBase
    {
        private readonly DbContextRender _context;

        public IndustryController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Industry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Industry>>> GetIndustries()
        {
          if (_context.Industry == null)
          {
              return NotFound();
          }
            return await _context.Industry.ToListAsync();
        }

        // GET: api/Industry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Industry>> GetIndustry(int id)
        {
          if (_context.Industry == null)
          {
              return NotFound();
          }
            var industry = await _context.Industry.FindAsync(id);

            if (industry == null)
            {
                return NotFound();
            }

            return industry;
        }

        // PUT: api/Industry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndustry(int id, Industry industry)
        {
            if (id != industry.Id)
            {
                return BadRequest();
            }

            _context.Entry(industry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndustryExists(id))
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

        // POST: api/Industry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Industry>> PostIndustry(Industry industry)
        {
          if (_context.Industry == null)
          {
              return Problem("Entity set 'DbContextRender.Industries'  is null.");
          }
            _context.Industry.Add(industry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIndustry", new { id = industry.Id }, industry);
        }

        // DELETE: api/Industry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndustry(int id)
        {
            if (_context.Industry == null)
            {
                return NotFound();
            }
            var industry = await _context.Industry.FindAsync(id);
            if (industry == null)
            {
                return NotFound();
            }

            _context.Industry.Remove(industry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IndustryExists(int id)
        {
            return (_context.Industry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

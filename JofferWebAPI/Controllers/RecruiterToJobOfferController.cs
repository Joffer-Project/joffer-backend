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
    public class RecruiterToJobOfferController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RecruiterToJobOfferController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/RecruiterToJobOffer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecruiterToJobOffer>>> GetRecruiterToJobOffers()
        {
          if (_context.RecruiterToJobOffers == null)
          {
              return NotFound();
          }
            return await _context.RecruiterToJobOffers.ToListAsync();
        }

        // GET: api/RecruiterToJobOffer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecruiterToJobOffer>> GetRecruiterToJobOffer(int id)
        {
          if (_context.RecruiterToJobOffers == null)
          {
              return NotFound();
          }
            var recruiterToJobOffer = await _context.RecruiterToJobOffers.FindAsync(id);

            if (recruiterToJobOffer == null)
            {
                return NotFound();
            }

            return recruiterToJobOffer;
        }

        // PUT: api/RecruiterToJobOffer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecruiterToJobOffer(int id, RecruiterToJobOffer recruiterToJobOffer)
        {
            if (id != recruiterToJobOffer.Id)
            {
                return BadRequest();
            }

            _context.Entry(recruiterToJobOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruiterToJobOfferExists(id))
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

        // POST: api/RecruiterToJobOffer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecruiterToJobOffer>> PostRecruiterToJobOffer(RecruiterToJobOffer recruiterToJobOffer)
        {
          if (_context.RecruiterToJobOffers == null)
          {
              return Problem("Entity set 'MyDbContext.RecruiterToJobOffers'  is null.");
          }
            _context.RecruiterToJobOffers.Add(recruiterToJobOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecruiterToJobOffer", new { id = recruiterToJobOffer.Id }, recruiterToJobOffer);
        }

        // DELETE: api/RecruiterToJobOffer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruiterToJobOffer(int id)
        {
            if (_context.RecruiterToJobOffers == null)
            {
                return NotFound();
            }
            var recruiterToJobOffer = await _context.RecruiterToJobOffers.FindAsync(id);
            if (recruiterToJobOffer == null)
            {
                return NotFound();
            }

            _context.RecruiterToJobOffers.Remove(recruiterToJobOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecruiterToJobOfferExists(int id)
        {
            return (_context.RecruiterToJobOffers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

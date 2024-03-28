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
    public class RecruiterToJobOfferController : ControllerBase
    {
        private readonly DbContextRender _context;

        public RecruiterToJobOfferController(DbContextRender context)
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
        public async Task<IActionResult> PutRecruiterToJobOffer(int id, RecruiterToJobOfferDto recruiterToJobOfferDto)
        {
            RecruiterToJobOffer recruiterToJobOffer = new(recruiterToJobOfferDto)
            {
                Id = id,
            };

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
        public async Task<ActionResult<RecruiterToJobOfferDto>> PostRecruiterToJobOffer(RecruiterToJobOfferDto recruiterToJobOfferDto)
        {
            if (_context.RecruiterToJobOffers == null)
            {
                return Problem("Entity set 'MyDbContext.RecruiterToJobOffers'  is null.");
            }
            _context.RecruiterToJobOffers.Add(new RecruiterToJobOffer(recruiterToJobOfferDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecruiterToJobOffer", new { id = recruiterToJobOfferDto.Id }, recruiterToJobOfferDto);
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

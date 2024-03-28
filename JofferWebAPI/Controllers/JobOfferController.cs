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
    public class JobOfferController : ControllerBase
    {
        private readonly DbContextRender _context;

        public JobOfferController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/JobOffer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetJobOffers()
        {
          if (_context.JobOffers == null)
          {
              return NotFound();
          }
            return await _context.JobOffers.ToListAsync();
        }

        // GET: api/JobOffer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobOffer>> GetJobOffer(int id)
        {
          if (_context.JobOffers == null)
          {
              return NotFound();
          }
            var jobOffer = await _context.JobOffers.FindAsync(id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            return jobOffer;
        }

        // PUT: api/JobOffer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobOffer(int id, JobOfferDto jobOfferDto)
        {
            JobOffer jobOffer = new(jobOfferDto)
            {
                Id = id,
            };

            if (id != jobOffer.Id)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<JobOfferDto>> PostJobOffer(JobOfferDto jobOfferDto)
        {
          if (_context.JobOffers == null)
          {
              return Problem("Entity set 'MyDbContext.JobOffers'  is null.");
          }
            _context.JobOffers.Add(new JobOffer(jobOfferDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobOffer", new { id = jobOfferDto.Id }, jobOfferDto);
        }

        // DELETE: api/JobOffer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobOffer(int id)
        {
            if (_context.JobOffers == null)
            {
                return NotFound();
            }
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobOfferExists(int id)
        {
            return (_context.JobOffers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

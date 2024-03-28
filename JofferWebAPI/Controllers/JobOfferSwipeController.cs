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
    public class JobOfferSwipeController : ControllerBase
    {
        private readonly DbContextRender _context;

        public JobOfferSwipeController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/JobOfferSwipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOfferSwipe>>> GetJobOfferSwipes()
        {
          if (_context.JobOfferSwipes == null)
          {
              return NotFound();
          }
            return await _context.JobOfferSwipes.ToListAsync();
        }

        // GET: api/JobOfferSwipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobOfferSwipe>> GetJobOfferSwipe(int id)
        {
          if (_context.JobOfferSwipes == null)
          {
              return NotFound();
          }
            var jobOfferSwipe = await _context.JobOfferSwipes.FindAsync(id);

            if (jobOfferSwipe == null)
            {
                return NotFound();
            }

            return jobOfferSwipe;
        }

        // PUT: api/JobOfferSwipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobOfferSwipe(int id, JobOfferSwipeDto jobOfferSwipeDto)
        {
            JobOfferSwipe jobOfferSwipe = new(jobOfferSwipeDto)
            {
                Id = id,
            };

            if (id != jobOfferSwipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobOfferSwipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobOfferSwipeExists(id))
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

        // POST: api/JobOfferSwipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobOfferSwipeDto>> PostJobOfferSwipe(JobOfferSwipeDto jobOfferSwipeDto)
        {
          if (_context.JobOfferSwipes == null)
          {
              return Problem("Entity set 'MyDbContext.JobOfferSwipes'  is null.");
          }
            _context.JobOfferSwipes.Add(new JobOfferSwipe(jobOfferSwipeDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobOfferSwipe", new { id = jobOfferSwipeDto.Id }, jobOfferSwipeDto);
        }

        // DELETE: api/JobOfferSwipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobOfferSwipe(int id)
        {
            if (_context.JobOfferSwipes == null)
            {
                return NotFound();
            }
            var jobOfferSwipe = await _context.JobOfferSwipes.FindAsync(id);
            if (jobOfferSwipe == null)
            {
                return NotFound();
            }

            _context.JobOfferSwipes.Remove(jobOfferSwipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobOfferSwipeExists(int id)
        {
            return (_context.JobOfferSwipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

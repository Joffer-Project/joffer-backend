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
    public class RecruiterController : ControllerBase
    {
        private readonly DbContextRender _context;

        public RecruiterController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Recruiter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recruiter>>> GetRecruiters()
        {
          if (_context.Recruiters == null)
          {
              return NotFound();
          }
            return await _context.Recruiters.ToListAsync();
        }

        // GET: api/Recruiter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recruiter>> GetRecruiter(int id)
        {
          if (_context.Recruiters == null)
          {
              return NotFound();
          }
            var recruiter = await _context.Recruiters.FindAsync(id);

            if (recruiter == null)
            {
                return NotFound();
            }

            return recruiter;
        }

        // PUT: api/Recruiter/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecruiter(int id, RecruiterDto recruiterDto)
        {
            Recruiter recruiter = new(recruiterDto)
            {
                Id = id,
            };

            if (id != recruiter.Id)
            {
                return BadRequest();
            }

            _context.Entry(recruiter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruiterExists(id))
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

        // POST: api/Recruiter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecruiterDto>> PostRecruiter(RecruiterDto recruiterDto)
        {
          if (_context.Recruiters == null)
          {
              return Problem("Entity set 'MyDbContext.Recruiters'  is null.");
          }
            _context.Recruiters.Add(new Recruiter(recruiterDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecruiter", new { id = recruiterDto.Id }, recruiterDto);
        }

        // DELETE: api/Recruiter/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruiter(int id)
        {
            if (_context.Recruiters == null)
            {
                return NotFound();
            }
            var recruiter = await _context.Recruiters.FindAsync(id);
            if (recruiter == null)
            {
                return NotFound();
            }

            _context.Recruiters.Remove(recruiter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecruiterExists(int id)
        {
            return (_context.Recruiters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

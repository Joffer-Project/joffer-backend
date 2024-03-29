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
    public class ApplicantController : ControllerBase
    {
        private readonly DbContextRender _context;

        public ApplicantController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Applicant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
          if (_context.Applicants == null)
          {
              return NotFound();
          }
            return await _context.Applicants.ToListAsync();
        }

        // GET: api/Applicant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicant(int id)
        {
          if (_context.Applicants == null)
          {
              return NotFound();
          }
            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }

            return applicant;
        }

        // PUT: api/Applicant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicant(int id, ApplicantDto applicantDto)
        {
            Applicant applicant = new(applicantDto)
            {
                Id = id,
            };

            if (id != applicant.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
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

        // POST: api/Applicant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicantDto>> PostApplicant(ApplicantDto applicantDto)
        {
          if (_context.Applicants == null)
          {
              return Problem("Entity set 'MyDbContext.Applicants'  is null.");
          }
            _context.Applicants.Add(new Applicant(applicantDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicant", new { id = applicantDto.Id }, applicantDto);
        }

        // DELETE: api/Applicant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            if (_context.Applicants == null)
            {
                return NotFound();
            }
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicantExists(int id)
        {
            return (_context.Applicants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly DbContextRender _context;

        public DisciplineController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Discipline
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dicipline>>> GetDiciplines()
        {
          if (_context.Diciplines == null)
          {
              return NotFound();
          }
            return await _context.Diciplines.ToListAsync();
        }

        // GET: api/Discipline/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dicipline>> GetDicipline(int id)
        {
          if (_context.Diciplines == null)
          {
              return NotFound();
          }
            var dicipline = await _context.Diciplines.FindAsync(id);

            if (dicipline == null)
            {
                return NotFound();
            }

            return dicipline;
        }

        // PUT: api/Dicipline/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDicipline(int id, DiciplineDto diciplineDto)
        {
            Dicipline dicipline = new(diciplineDto)
            {
                Id = id
            };

            if (id != dicipline.Id)
            {
                return BadRequest();
            }

            _context.Entry(dicipline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiciplineExists(id))
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

        // POST: api/Dicipline
        [HttpPost]
        public async Task<ActionResult<DiciplineDto>> PostDicipline(DiciplineDto diciplineDto)
        {
            if (_context.Diciplines == null)
            {
                return Problem("Entity set 'MyDbContext.Diciplines'  is null.");
            }

            _context.Diciplines.Add(new Dicipline(diciplineDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDicipline", new { id = diciplineDto.Id }, diciplineDto);
        }

        // DELETE: api/Discipline/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDicipline(int id)
        {
            if (_context.Diciplines == null)
            {
                return NotFound();
            }
            var dicipline = await _context.Diciplines.FindAsync(id);
            if (dicipline == null)
            {
                return NotFound();
            }

            _context.Diciplines.Remove(dicipline);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiciplineExists(int id)
        {
            return (_context.Diciplines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

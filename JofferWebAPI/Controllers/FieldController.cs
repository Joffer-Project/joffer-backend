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
    public class FieldController : ControllerBase
    {
        private readonly DbContextRender _context;

        public FieldController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Field
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Field>>> GetFields()
        {
          if (_context.Fields == null)
          {
              return NotFound();
          }
            return await _context.Fields.ToListAsync();
        }

        // GET: api/Field/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> GetField(int id)
        {
          if (_context.Fields == null)
          {
              return NotFound();
          }
            var @field = await _context.Fields.FindAsync(id);

            if (@field == null)
            {
                return NotFound();
            }

            return @field;
        }

        // PUT: api/Field/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutField(int id, Field @field)
        {
            if (id != @field.Id)
            {
                return BadRequest();
            }

            _context.Entry(@field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(id))
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

        // POST: api/Field
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Field>> PostField(Field @field)
        {
          if (_context.Fields == null)
          {
              return Problem("Entity set 'MyDbContext.Fields'  is null.");
          }
            _context.Fields.Add(@field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetField", new { id = @field.Id }, @field);
        }

        // DELETE: api/Field/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            if (_context.Fields == null)
            {
                return NotFound();
            }
            var @field = await _context.Fields.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FieldExists(int id)
        {
            return (_context.Fields?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

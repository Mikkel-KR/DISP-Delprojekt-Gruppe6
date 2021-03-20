using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Data.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaandvaerkerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HaandvaerkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Haandvaerker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Haandvaerker>>> GetHaandvaerkerSet()
        {
            return await _context.HaandvaerkerSet.ToListAsync();
        }

        // GET: api/Haandvaerker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Haandvaerker>> GetHaandvaerker(int id)
        {
            var haandvaerker = await _context.HaandvaerkerSet.FindAsync(id);

            if (haandvaerker == null)
            {
                return NotFound();
            }

            return haandvaerker;
        }

        // PUT: api/Haandvaerker/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHaandvaerker(int id, Haandvaerker haandvaerker)
        {
            if (id != haandvaerker.HaandvaerkerId)
            {
                return BadRequest();
            }

            _context.Entry(haandvaerker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HaandvaerkerExists(id))
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

        // POST: api/Haandvaerker
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Haandvaerker>> PostHaandvaerker(Haandvaerker haandvaerker)
        {
            _context.HaandvaerkerSet.Add(haandvaerker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHaandvaerker", new { id = haandvaerker.HaandvaerkerId }, haandvaerker);
        }

        // DELETE: api/Haandvaerker/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Haandvaerker>> DeleteHaandvaerker(int id)
        {
            var haandvaerker = await _context.HaandvaerkerSet.FindAsync(id);
            if (haandvaerker == null)
            {
                return NotFound();
            }

            _context.HaandvaerkerSet.Remove(haandvaerker);
            await _context.SaveChangesAsync();

            return haandvaerker;
        }

        private bool HaandvaerkerExists(int id)
        {
            return _context.HaandvaerkerSet.Any(e => e.HaandvaerkerId == id);
        }
    }
}

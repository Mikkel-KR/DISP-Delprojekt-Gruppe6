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
    public class VaerktoejController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VaerktoejController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vaerktoejs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaerktoej>>> GetVaerktoejSet()
        {
            return await _context.Vaerktoej.ToListAsync();
        }

        // GET: api/Vaerktoejs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaerktoej>> GetVaerktoej(long id)
        {
            var vaerktoej = await _context.Vaerktoej.FindAsync(id);

            if (vaerktoej == null)
            {
                return NotFound();
            }

            return vaerktoej;
        }

        // PUT: api/Vaerktoejs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaerktoej(long id, Vaerktoej vaerktoej)
        {
            if (id != vaerktoej.VTId)
            {
                return BadRequest();
            }

            _context.Entry(vaerktoej).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaerktoejExists(id))
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

        // POST: api/Vaerktoejs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vaerktoej>> PostVaerktoej(Vaerktoej vaerktoej)
        {
            _context.Vaerktoej.Add(vaerktoej);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaerktoej", new { id = vaerktoej.VTId }, vaerktoej);
        }

        // DELETE: api/Vaerktoejs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vaerktoej>> DeleteVaerktoej(long id)
        {
            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            _context.Vaerktoej.Remove(vaerktoej);
            await _context.SaveChangesAsync();

            return vaerktoej;
        }

        private bool VaerktoejExists(long id)
        {
            return _context.Vaerktoej.Any(e => e.VTId == id);
        }
    }
}

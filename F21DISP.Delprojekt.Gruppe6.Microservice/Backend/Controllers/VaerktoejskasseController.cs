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
    public class VaerktoejskasseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VaerktoejskasseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vaerktoejskasse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaerktoejskasse>>> GetVaerktoejskasseSet()
        {
            return await _context.VaerktoejskasseSet.ToListAsync();
        }

        // GET: api/Vaerktoejskasse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaerktoejskasse>> GetVaerktoejskasse(int id)
        {
            var vaerktoejskasse = await _context.VaerktoejskasseSet.FindAsync(id);

            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return vaerktoejskasse;
        }

        // PUT: api/Vaerktoejskasse/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaerktoejskasse(int id, Vaerktoejskasse vaerktoejskasse)
        {
            if (id != vaerktoejskasse.VTKId)
            {
                return BadRequest();
            }

            _context.Entry(vaerktoejskasse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaerktoejskasseExists(id))
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

        // POST: api/Vaerktoejskasse
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vaerktoejskasse>> PostVaerktoejskasse(Vaerktoejskasse vaerktoejskasse)
        {
            _context.VaerktoejskasseSet.Add(vaerktoejskasse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaerktoejskasse", new { id = vaerktoejskasse.VTKId }, vaerktoejskasse);
        }

        // DELETE: api/Vaerktoejskasse/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vaerktoejskasse>> DeleteVaerktoejskasse(int id)
        {
            var vaerktoejskasse = await _context.VaerktoejskasseSet.FindAsync(id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            _context.VaerktoejskasseSet.Remove(vaerktoejskasse);
            await _context.SaveChangesAsync();

            return vaerktoejskasse;
        }

        private bool VaerktoejskasseExists(int id)
        {
            return _context.VaerktoejskasseSet.Any(e => e.VTKId == id);
        }
    }
}

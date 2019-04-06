using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupsController : ControllerBase
    {
        private readonly FillAgainContext _context;

        public CupsController(FillAgainContext context)
        {
            _context = context;
        }

        // GET: api/Cups
        [HttpGet]
        public IEnumerable<Cup> GetCups()
        {
            return _context.Cups;
        }

        // GET: api/Cups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCup([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cup = await _context.Cups.FindAsync(id);

            Console.WriteLine(cup);

            if (cup == null)
            {
                return NotFound();
            }

            return Ok(cup);
        }

        // PUT: api/Cups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCup([FromRoute] string id, [FromBody] Cup cup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cup.Id)
            {
                return BadRequest();
            }

            _context.Entry(cup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CupExists(id))
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

       

        // POST: api/Cups
        [HttpPost]
        public async Task<IActionResult> PostCup([FromBody] Cup cup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cups.Add(cup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCup", new { id = cup.Id }, cup);
        }

        // DELETE: api/Cups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCup([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cup = await _context.Cups.FindAsync(id);
            if (cup == null)
            {
                return NotFound();
            }

            _context.Cups.Remove(cup);
            await _context.SaveChangesAsync();

            return Ok(cup);
        }

        private bool CupExists(string id)
        {
            return _context.Cups.Any(e => e.Id == id);
        }
    }
}
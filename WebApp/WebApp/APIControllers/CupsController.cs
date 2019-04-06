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
        public List<Cup> GetCups()
        {
            Console.Write(_context.Cups.ToList());
            return _context.Cups.ToList();
        }
        
        
        //Gundars checks if cup has money
        // GET: api/Cups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCup([FromRoute] string id)
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

 
            int tmplimit;
            if (cup.limit != null)
            {
                tmplimit = Int32.Parse(cup.limit);
            }
            else
            {
                return BadRequest(ModelState);
            }


            int newLimit = tmplimit - 10;
            if(tmplimit > 10)
            {
                //call danske bank api
                if(true)
                {
                    cup.limit = newLimit.ToString();
                    _context.Entry(cup).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            Console.WriteLine(cup);

          

            return Ok(cup);
        }



        // PUT: api/Cups/5
        [HttpPut("topup/{id}/{money}")]
        public async Task<IActionResult> PutCup([FromRoute] string id,[FromRoute] string money)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cup d = _context.Cups.Where(s => s.Id == id).First();

            int tmpmoney = Int32.Parse(d.limit);
            int newlimit = tmpmoney + Int32.Parse(money);
            d.limit = newlimit.ToString();

           // foreach (Cup c in _context.Cups.Where(r => r.Id == cup.Id))
            //{
        //     c.limit = newlimit.ToString();
//            }

            

           
            Console.WriteLine(d);
            // var cupas = _context.Cups.Where(s => s.Id == cup.Id).First();
            //cupas.limit = newlimit.ToString();



            _context.Entry(d).State= EntityState.Modified;
            _context.SaveChanges();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }


        [HttpPut("define/{cid}/{uid}")]
        public async Task<IActionResult> DefineCup([FromRoute] string cid, [FromRoute] string uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cup d = _context.Cups.Where(s => s.Id == cid).First();

            if(d.owner != null)
            {
                return BadRequest();
            }
            d.owner = uid;

            Console.WriteLine(d);
            


            _context.Entry(d).State = EntityState.Modified;
            _context.SaveChanges();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return Ok(d);
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
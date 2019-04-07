using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Data.Entities;
using RestSharp;
using RestSharp.Authenticators;

namespace WebApp.APIControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CupsController : ControllerBase
    {
        private readonly FillAgainContext _context;
        public HttpClient client;
        private Helper helper;
        public HttpResponseMessage responseMessage {get; set;}
 
        //object only to create array for json
        public class DuckingCupList
        {
            public ICollection<Cup> cuparray {get; set;}
        }
        public CupsController(FillAgainContext context)
        {
            _context = context;
       
            
        }

        // GET: api/Cups
        [HttpGet]
        public string GetCups()
        {
            ICollection<Cup> cupCol = _context.Cups.ToList();
            DuckingCupList dcknglist = new DuckingCupList { cuparray = cupCol };
            string json = JsonConvert.SerializeObject(dcknglist);

            return json;
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
                var client = new RestClient("https://api.sandbox.mobilepay.dk/bindings-restapi/api/v1/payments/payout-bankaccount");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Postman-Token", "d8183387-6050-45d4-a047-aee929de826a");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("x-ibm-client-secret", "L7yW0eV0eK5yX1nK4rO0lI8sX5aN2tL6aQ0sL7gM1xO6sW8kK1");
                request.AddHeader("x-ibm-client-id", "1c0cd3ff-1143-476b-b136-efe9b1f5ecf3");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("undefined", "{\n\t\"merchantId\" : \"35ed5788-51e3-4fd8-b7a3-020a9f7c8533\",\n\t\"merchantBinding\" : \"000\",\n\t\"receiverRegNumber\" : \"3098\",\n\t\"receiverAccountNumber\" : \"3100460793\",\n\t\"amount\" : 10\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);


                //if danskeAPI went okay we subtract the limit
                if (response.Equals(204))
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
            

            Console.WriteLine(d);
       
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
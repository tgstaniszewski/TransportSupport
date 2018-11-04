using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportSupport;
using TransportSupport.Models;

namespace TransportSupport.Controllers
{
    [Produces("application/json")]
    [Route("api/Freights")]
    public class FreightsController : Controller
    {
        private readonly EFCContext _context;

        public FreightsController(EFCContext context)
        {
            _context = context;
        }

        // GET: api/Freights
        [HttpGet]
        public JsonResult /*IEnumerable*/ GetFreights()
        {
            var listFreights = _context.Freights;
            
            return Json(listFreights.ToList());
        }

        // GET: api/Freights/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFreight([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var freight = await _context.Freights.SingleOrDefaultAsync(m => m.ID == id);

            if (freight == null)
            {
                return NotFound();
            }

            return Ok(Json(freight));
        }

        // PUT: api/Freights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFreight([FromRoute] int id, [FromBody] Freight freight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != freight.ID)
            {
                return BadRequest();
            }

            _context.Entry(freight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreightExists(id))
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

        // POST: api/Freights
        [HttpPost]
        public async Task<IActionResult> PostFreight([FromBody] Freight freight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Freights.Add(freight);
            await _context.SaveChangesAsync();

            var newFreightInvoice = new FreightInvoice
            {
                FreightId = freight.ID,
                InvoiceId = null
            };

            _context.FreightInvoices.Add(newFreightInvoice);
            await _context.SaveChangesAsync();
            
            return Ok(Json(freight.ID));
        }

        // DELETE: api/Freights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreight([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var freight = await _context.Freights.SingleOrDefaultAsync(m => m.ID == id);
            var fraightInvoice = await _context.FreightInvoices.SingleOrDefaultAsync(m => m.FreightId == id);

            if (fraightInvoice == null)
            {
                return NotFound();
            }

            if (freight == null)
            {
                return NotFound();
            }

            _context.FreightInvoices.Remove(fraightInvoice);
            _context.Freights.Remove(freight);
            
            await _context.SaveChangesAsync();

            return Ok(freight);
        }

        private bool FreightExists(int id)
        {
            return _context.Freights.Any(e => e.ID == id);
        }
    }
}
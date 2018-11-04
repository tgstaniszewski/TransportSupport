using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransportSupport;
using TransportSupport.Models;

namespace TransportSupport.Controllers
{
    public class TracksController : Controller
    {
        private readonly EFCContext _context;

        public TracksController(EFCContext context)
        {
            _context = context;
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            ViewBag.newInvoicesCount = _context.FreightInvoices
                .Where(i => i.InvoiceId == null)
                .GroupBy(i => i.Freight.Track.Contractor).Count();
            var tracks = _context.Tracks.Include(t => t.Contractor);
            return View(await tracks.ToListAsync());
        }



        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Contractor)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {

            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "Name");
            return View();
        }

        // POST: Tracks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ContractorID,UrlGoogleMaps,Time,Distance,Price,UnitOfMeasure")] Track track)
        {
            if (ModelState.IsValid)
            {
                _context.Add(track);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "Name", track.ContractorID);
            return View(track);
        }

        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.SingleOrDefaultAsync(m => m.ID == id);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "Name", track.ContractorID);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ContractorID,UrlGoogleMaps,Time,Distance,Price,UnitOfMeasure")] Track track)
        {
            if (id != track.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "Name", track.ContractorID);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Contractor)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var track = await _context.Tracks.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(int id)
        {
            return _context.Tracks.Any(e => e.ID == id);
        }
    }
}

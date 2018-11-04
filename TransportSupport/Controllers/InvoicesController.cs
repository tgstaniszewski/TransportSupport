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
    public class InvoicesController : Controller
    {
        private readonly EFCContext _context;

        public InvoicesController(EFCContext context)
        {
            _context = context;
        }




        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = _context.Invoices.Include(m=>m.Company).Include(m=>m.Contractor);
            ViewBag.Contractors = _context.Contractors;
           
            ViewBag.newInvoicesCount = _context.FreightInvoices
                .Where(i => i.InvoiceId == null)
                .GroupBy(i => i.Freight.Track.Contractor).Count();

            return View(await invoices.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Company)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {

            ViewData["Company"] = new SelectList(_context.Companies, "ID", "Name");

            ViewBag.Contractor = new SelectList(_context.FreightInvoices
                    .Where(i => i.InvoiceId == null)
                    .Select(i => i.Freight.Track.Contractor)
                    .Distinct()
                , "ID", "Name");

            var compId = _context.Companies.ToList();

            var model = new Invoice();
            if (compId.Count != 0)
            {
                model.InvoiceDate = DateTime.Now;
                model.CompanyId = compId[0].ID; // domyślny pierwszy wpis

            }

            return View(model);
        }



        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InvoiceDate,CompanyId,InvoiceNumber,ContractorId,MethodOfPayment")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();

                //zapis do bazy łaczącej faktury z frachtami
                var arrayFreightInvoices = _context.FreightInvoices
                    .Where(i => i.InvoiceId == null & i.Freight.Track.ContractorID == invoice.ContractorId);
                foreach (var itemFreightInvoice in arrayFreightInvoices)
                {
                    itemFreightInvoice.InvoiceId = invoice.ID;
                    _context.Entry(itemFreightInvoice).State = EntityState.Modified;

                    //_context.FreightInvoices.Add(itemFreightInvoice);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Company"] = new SelectList(_context.Companies, "ID", "Name");

            ViewBag.Contractor = new SelectList(_context.FreightInvoices
                    .Where(i => i.InvoiceId == null)
                    .Select(i => i.Freight.Track.Contractor)
                    .Distinct()
                , "ID", "Name");

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "ID", "ID", invoice.CompanyId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InvoiceDate,CompanyId,InvoiceNumber,ContractorsId,MethodOfPayment")] Invoice invoice)
        {
            if (id != invoice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.ID))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "ID", "ID", invoice.CompanyId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Company)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var freightInvoice = _context.FreightInvoices .Where(m => m.InvoiceId == id);
            foreach (var itemFreightInvoice in freightInvoice)
            {
                itemFreightInvoice.InvoiceId = null;
                _context.Entry(itemFreightInvoice).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.ID == id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //GET: Invoices/GetFreightInvoicesNull/5 - pobieranie frachtów nie przypisanych do żadnej faktury dla wybranego kontrachenta
        public async Task<IActionResult> GetFreightInvoicesNull([FromRoute]int id)
        {
            var customerFreightNull = await _context.FreightInvoices.Where(m => m.InvoiceId == null & m.Freight.Track.ContractorID == id).Select(m => m.Freight.Track).ToListAsync();
            if (customerFreightNull.Count == 0)
            {
                return NotFound();
            }
            return Json(customerFreightNull);
        }

        public IActionResult GetNumberIvoice()
        {
            var listNumberIvoice= _context.Invoices.Select(m => m.InvoiceNumber).ToList();
            if (listNumberIvoice.Count == 0)
            {
                return Ok(1);
            }
            var lastIndex = listNumberIvoice.Count - 1;
            var lastNumber = Int16.Parse(listNumberIvoice[lastIndex]);
            return Ok((lastNumber+1));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.ID == id);
        }
    }
}

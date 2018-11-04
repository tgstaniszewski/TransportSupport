using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace TransportSupport.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly EFCContext _context;
        private readonly UserManager<IdentityUser> _identityUser;

        public CalendarController(EFCContext context, UserManager<IdentityUser> identityUser)
        {
            _identityUser = identityUser;
            _context = context;
        }

        // GET: Calendar
        public ActionResult Index()
        {
            ViewBag.newInvoicesCount = _context.FreightInvoices
                .Where(i => i.InvoiceId == null)
                .GroupBy(i => i.Freight.Track.Contractor).Count();

            var listContractor = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "",
                    Value = ""
                }
            };
            foreach (var contractor in _context.Contractors)
            {
                listContractor.Add(new SelectListItem()
                {
                    Text = contractor.Name ,
                    Value = contractor.ID.ToString()

                });
            }

            
            ViewBag.contractor = listContractor;
            return View();
        }

        // GET: Calendar/Tracks/5 - list Json
        public async Task<IActionResult> Tracks(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracks = await _context.Tracks.Where(m => m.ID == id).ToListAsync();

            if (tracks.Count == 0)
            {
                return NotFound();
            }

            return Json(tracks);
        }

        // GET: Calendar/TracksCustomer/5 - list Json
        public async Task<IActionResult> TracksCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracks = await _context.Tracks.Where(m => m.ContractorID == id).ToListAsync();

            if (tracks.Count == 0)
            {
                return NotFound();
            }

            return Json(tracks);
        }

        
    }
}
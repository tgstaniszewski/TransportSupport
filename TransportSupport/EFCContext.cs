using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TransportSupport.Models;

namespace TransportSupport
{
    public class EFCContext: IdentityDbContext
    {
        public EFCContext(DbContextOptions<EFCContext> opt) : base(opt)
        {

        }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Freight> Freights { get; set; }

        public DbSet<FreightInvoice> FreightInvoices { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Company>  Companies { get; set; }
    }
}

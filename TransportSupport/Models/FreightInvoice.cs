using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace TransportSupport.Models
{
    public class FreightInvoice
    {
        public int ID { get; set; }

        public int? InvoiceId { get; set; }

        public int FreightId { get; set; }

        [ForeignKey("FreightId")]
        public Freight Freight { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }
    }
}

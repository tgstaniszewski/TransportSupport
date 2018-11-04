using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace TransportSupport.Models
{
    public class Freight
    {
        public int ID { get; set; }

        public int TrackId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Description { get; set; }

        [ForeignKey("TrackId")]
        public Track Track { get; set; }

        public ICollection<FreightInvoice> FreightInvoice { get; set; }

    }
}

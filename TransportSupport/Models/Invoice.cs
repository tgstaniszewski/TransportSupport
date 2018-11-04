using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using System.ComponentModel;

namespace TransportSupport.Models
{
    public class Invoice
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Data wystawienia")]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("Sprzedawca")]
        public int CompanyId { get; set; }

        [DisplayName("Numer faktury")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Nabywca")]
        public int ContractorId { get; set; }

        [DisplayName("Metoda zapłaty")]
        public string MethodOfPayment { get; set; }

       [ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public ICollection<FreightInvoice> FreightInvoices { get; set; }

    }
}

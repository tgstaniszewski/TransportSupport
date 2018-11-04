using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace TransportSupport.Models
{
    public class Contractor
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Ulica")]
        public string Street { get; set; }
        //[Required]
        [DisplayName("Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        [RegularExpression("[0-9]{2}-[0-9]{3}")]
        public string PostCode { get; set; }
        [Required]
        [DisplayName("Miasto")]
        public string City { get; set; }
        [Required]
        public string NIP { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Telefon")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        //public ICollection<Invoice> Invoices { get; set; }
        //public ICollection<Track> Tracks { get; set; }
    }
}

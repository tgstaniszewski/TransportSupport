using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransportSupport.Models
{
    public class Company
    {
        public int ID { get; set; }

        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Ulica")]
        public string Street { get; set; }
        [DisplayName("Miasto")]
        public string City { get; set; }

        public string NIP { get; set; }
        [DisplayName("telefon")]
        public int Phone { get; set; }
        [DisplayName("Numer Konta Bankowego")]
        public int BankAccountNumber { get; set; }

        //[ForeignKey("ContractorID")]
        //public Contractor Contractor { get; set; }
    }
}

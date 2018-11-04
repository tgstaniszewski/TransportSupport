using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TransportSupport.Models
{
    

    public class Track
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [DisplayName("Kontrahent")]
        [Required]
        public int ContractorID { get; set; }

        [DisplayName("Navigaton link")]
        public string UrlGoogleMaps { get; set; }

        [Required]
        [DisplayName("Czas przejazdu")]
        public TimeSpan Time { get; set; }

        [Required]
        [DisplayName("Dystans")]
        public  double  Distance { get; set; }
        [Required]
        [DisplayName("Cena")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Jednostka miary")]
        public string UnitOfMeasure { get; set; }

        [ForeignKey("ContractorID")]
        public Contractor Contractor { get; set; }
        //public Freight Freight { get; set; }
        public ICollection<Freight> Freights { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public partial class Haandvaerker
    {
        public Haandvaerker()
        {
            Vaerktoejskasse = new HashSet<Vaerktoejskasse>();
        }
        [Key]
        public int HaandvaerkerId { get; set; }
        [Display(Name = "Ansættelses dato")]
        public DateTime HVAnsaettelsedato { get; set; }
        [Display(Name = "Efternavn")]
        public string HVEfternavn { get; set; }
        [Display(Name = "Fagområde")]
        public string HVFagomraade { get; set; }
        [Display(Name = "Fornavn")]
        public string HVFornavn { get; set; }
        public HashSet<Vaerktoejskasse> Vaerktoejskasse { get; set; }
    }
}

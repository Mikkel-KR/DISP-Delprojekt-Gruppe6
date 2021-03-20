using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public partial class Vaerktoejskasse
    {
        public Vaerktoejskasse()
        {
            Vaerktoej = new HashSet<Vaerktoej>();
        }
        [Key]
        public int VTKId { get; set; }
        [Display(Name = "Anskaffet")]
        public DateTime VTKAnskaffet { get; set; }
        [Display(Name = "Fabrikat")]
        public string VTKFabrikat { get; set; }
        [Display(Name = "Ejes af")]
        public int? VTKEjesAf { get; set; }
        [Display(Name = "Model")]
        public string VTKModel { get; set; }
        [Display(Name = "Serienummer")]
        public string VTKSerienummer { get; set; }
        [Display(Name = "Farve")]
        public string VTKFarve { get; set; }
        public Haandvaerker EjesAfNavigation { get; set; }
        public HashSet<Vaerktoej> Vaerktoej { get; set; }
    }
}

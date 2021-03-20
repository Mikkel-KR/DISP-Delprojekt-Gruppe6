using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public partial class Vaerktoej
    {
        [Key]
        public long VTId { get; set; }
        [Display(Name = "Anskaffet")]
        public DateTime VTAnskaffet { get; set; }
        [Display(Name = "Fabrikat")]
        public string VTFabrikat { get; set; }
        [Display(Name = "Model")]
        public string VTModel { get; set; }
        [Display(Name = "Serie nr.")]
        public string VTSerienr { get; set; }
        [Display(Name = "Type")]
        public string VTType { get; set; }
        [Display(Name = "Ligger i")]
        public int? LiggerIvtk { get; set; }

        public Vaerktoejskasse LiggerIvtkNavigation { get; set; }
    }
}
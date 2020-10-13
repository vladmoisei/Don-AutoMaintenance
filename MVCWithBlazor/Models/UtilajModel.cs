using MVCWithBlazor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Models
{
    public class UtilajModel
    {
        [Display(Name = "Utilaj ID")]
        public int UtilajModelID { get; set; }

        [Required(ErrorMessage = "Numele utilaj este obligatoriu! ")]
        [StringLength(50, ErrorMessage = "Nume utilaj nu poate depasi 50 caractere.")]
        public string Utilaj { get; set; }

        [Display(Name = "Zona")]
        [Required(ErrorMessage = "Zona utilaj este obligatoriu! ")]
        public Zona ZonaUtilaj { get; set; }
    }
}

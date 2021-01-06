using MVCWithBlazor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Models
{
    public class ActiuneModel
    {
        [Display(Name = "Actiune ID")]
        public int ActiuneModelID { get; set; }

        [Required(ErrorMessage = "Desrierea este obligatorie! ")]
        [StringLength(500, ErrorMessage = "Descrierea nu poate depasi 500 caractere. ")]
        public string Descriere { get; set; }

        public TipActiune Tip { get; set; }

        [Display(Name ="Are Text?")]
        public bool IsUseInputText { get; set; }

        [StringLength(250, ErrorMessage = "Textul nu poate depasi 250 caractere. ")]
        [Display(Name ="Text")]
        public string? InputText { get; set; }

        [Display(Name = "Utilaj")]
        public int? UtilajModelID { get; set; }

        [Display(Name = "Utilaj")]
        public virtual UtilajModel UtilajModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Models
{
    public class ActiuneTemplateModel
    {
        [Display(Name = "Actiune ID")]
        public int ActiuneTemplateModelID { get; set; }

        [Required(ErrorMessage = "Desrierea este obligatorie! ")]
        [StringLength(250, ErrorMessage = "Descrierea nu poate depasi 250 caractere. ")]
        public string Descriere { get; set; }
        public bool IsUseInputText{ get; set; }

        [Required(ErrorMessage = "Desrierea este obligatorie! ")]
        [StringLength(250, ErrorMessage = "Descrierea nu poate depasi 250 caractere. ")]
        public string? InputText { get; set; }
    }
}

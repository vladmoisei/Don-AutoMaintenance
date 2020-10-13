using MVCWithBlazor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Models
{
    public class ProblemaModel
    {
        [Display(Name = "Problema ID")]
        public int ProblemaModelID { get; set; }

        [Required(ErrorMessage = "Data introducere este obligatorie.")]
        [Display(Name = "Data Introducere"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataIntroducere { get; set; }

        [Display(Name = "Utilaj")]
        public int? UtilajModelID { get; set; }

        [Display(Name = "Utilaj")]
        public virtual UtilajModel UtilajModel { get; set; }

        [Required(ErrorMessage = "Descriere problema este obligatorie! ")]
        [StringLength(250, ErrorMessage = "Descrierea nu poate depasi 250 de caractere! ")]
        [Display(Name = "Descriere Problema")]
        public string? ProblemaDescriere { get; set; }

        [Required(ErrorMessage = "Comentariu mentenanta este obligatorie! ")]
        [StringLength(250, ErrorMessage = "Comentariu mentenanta nu poate depasi 250 de caractere! ")]
        [Display(Name = "Comentariu Mentenanta")]
        public string? ComentariuMentenanta { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Status? Stare { get; set; }

        [Display(Name = "Responsabil")]
        public int? ResponsabilModelID { get; set; }
        
        [Display(Name = "Responsabil")]
        public virtual ResponsabilModel ResponsabilModel { get; set; }

        [Display(Name = "Termen Finalizare"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? TermenFinalizare { get; set; }

        [Display(Name = "Persoana update")]
        [Required(ErrorMessage = "Persoana care a modificat este obligatorie! ")]
        [StringLength(250, ErrorMessage = "Persoana care a modificat nu poate depasi 250 de caractere! ")]
        public string? LastPersonUpdateRow { get; set; }
    }
}

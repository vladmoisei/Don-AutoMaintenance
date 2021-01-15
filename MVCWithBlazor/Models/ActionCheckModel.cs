using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Models
{
    public class ActionCheckModel
    {
        [Display(Name = "Check ID")]
        public int ActionCheckModelID { get; set; }

        [Display(Name = "Data Luna zi"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime DataLucru { get; set; }
        public bool IsCheckedByOp { get; set; }

        [StringLength(5, ErrorMessage = "Textul nu poate depasi 5 caractere. ")]
        [Display(Name = "Text")]
        public string? InputText { get; set; }

        [Display(Name = "Actiune ID")]
        public int? ActiuneModelID { get; set; }

        [Display(Name = "Actiune")]
        public virtual ActiuneModel ActiuneModel { get; set; }

        [Display(Name = "Data Bifat operator"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime DataBifatByOp { get; set; }

        [StringLength(100, ErrorMessage = "Textul nu poate depasi 100 caractere. ")]
        [Display(Name = "Operator")]
        public string Username { get; set; }

        public bool IsCheckedBySef { get; set; }

        [StringLength(100, ErrorMessage = "Textul nu poate depasi 100 caractere. ")]

        [Display(Name = "Sef de schimb")]
        public string SefDeSchimb { get; set; }

        [Display(Name = "Data Bifat sef"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime DataBifatBySef { get; set; }

    }
}

using MVCWithBlazor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Models
{
    public class ResponsabilModel
    {
        [Display(Name = "Responsabil ID")]
        public int ResponsabilModelID { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu! ")]
        [StringLength(50, ErrorMessage = "Numele nu poate depasi 50 de caractere! ")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu! ")]
        [StringLength(50, ErrorMessage = "Prenumele nu poate depasi 50 de caractere! ")]
        public string Prenume { get; set; }

        [Display(Name = "Nume Persoana")]
        public string NumeComplet
        {
            get
            {
                return Prenume + " " + Nume;
            }
        }

        [Display(Name = "Adresa Email")]
        [Required(ErrorMessage = "Email este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Adresa de email invalida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Functia este obligatorie! ")]
        [StringLength(50, ErrorMessage = "Functia nu poate depasi 50 de caractere! ")]
        public string Functie { get; set; }

        [Required(ErrorMessage = "Departamentul este obligatoriu! ")]
        public Departament Departament { get; set; }
    }
}

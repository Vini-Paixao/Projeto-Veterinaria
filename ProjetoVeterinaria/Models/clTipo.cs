using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoVeterinaria.Models
{
    public class clTipo
    {
        [Display(Name = "Código")]
        public int codTipo { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório")]
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
    }
}
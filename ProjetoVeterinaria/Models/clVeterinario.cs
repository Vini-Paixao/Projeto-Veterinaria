using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoVeterinaria.Models
{
    public class clVeterinario
    {
        [Display(Name = "Código")]
        public int codVeterinario { get; set; }

        [Required(ErrorMessage = "O nome do veterinário é obrigatório")]
        [Display(Name = "Nome")]
        public string nomeVeterinario { get; set; }

        [Required(ErrorMessage = "O telefone do veterinário é obrigatório")]
        [Display(Name = "Telefone")]
        public string telVeterinario { get; set; }

        [Required(ErrorMessage = "O e-mail do veterinário é obrigatório")]
        [Display(Name = "E-mail")]
        public string EmailVeterinario { get; set; }
    }
}
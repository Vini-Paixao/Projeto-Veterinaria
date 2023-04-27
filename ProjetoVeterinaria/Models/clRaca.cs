using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoVeterinaria.Models
{
    public class clRaca
    {
        [Display(Name = "Código")]
        public int codRaca { get; set; }

        [Required(ErrorMessage = "O nome da raça é obrigatório")]
        [Display(Name = "Nome da Raça")]
        public string nomeRaca { get; set; }

        [Required(ErrorMessage = "O Tipo é obrigatório")]
        [Display(Name = "Código do Tipo")]
        public string codTipo { get; set; }

        [Display(Name = "Tipo do Animal")]
        public string tipo { get; set; }
    }
}
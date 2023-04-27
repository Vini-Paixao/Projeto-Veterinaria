using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoVeterinaria.Models
{
    public class clAnimal
    {
        [Display(Name = "Código")]
        public int codAnimal { get; set; }

        [Required(ErrorMessage = "O nome do Animal é obrigatório")]
        [Display(Name = "Nome")]
        public string nomeAnimal { get; set; }

        public string codRaca { get; set; }
        public string codCliente { get; set; }


        [Required(ErrorMessage = "A Raça é obrigatório")]
        [Display(Name = "Nome da Raça")]
        public string nomeRaca { get; set; }

        [Required(ErrorMessage = "O Cliente é obrigatório")]
        [Display(Name = "Nome do Cliente")]
        public string nomeCliente { get; set; }
    }
}
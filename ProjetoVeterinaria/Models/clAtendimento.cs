using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProjetoVeterinaria.Models
{
    public class clAtendimento
    {
        [Display(Name = "Código")]
        public int codAtendimento { get; set; }

        [Required(ErrorMessage = "A data é obrigatório")]
        [Display(Name = "Data")]
        public string dataAtendimento { get; set; }

        [Required(ErrorMessage = "A hora é obrigatório")]
        [Display(Name = "Hora")]
        public string horaAtendimento { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        [Display(Name = "Status")]
        public string statusAtendimento { get; set; }

        public string codAnimal { get; set; }
        public string codVeterinario { get; set; }


        [Required(ErrorMessage = "O animal é obrigatório")]
        [Display(Name = "Nome do Animal")]
        public string nomeAnimal { get; set; }

        [Required(ErrorMessage = "O veterinario é obrigatório")]
        [Display(Name = "Nome do Veterinario")]
        public string nomeVeterinario { get; set; }
    }
}
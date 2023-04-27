using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class clCliente
    {
            [Display(Name = "Código")]
            public int codCliente { get; set; }

            [Required(ErrorMessage = "O nome do cliente é obrigatório")]
            [Display(Name = "Nome")]
            public string nomeCliente { get; set; }

            [Required(ErrorMessage = "O telefone do cliente é obrigatório")]
            [Display(Name = "Telefone")]
            public string telCliente { get; set; }

            [Required(ErrorMessage = "O e-mail do cliente é obrigatório")]
            [Display(Name = "E-mail")]
            public string EmailCliente { get; set; }
    }
}
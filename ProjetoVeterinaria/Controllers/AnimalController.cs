using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Dados;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoVeterinaria.Controllers
{
    public class AnimalController : Controller
    {
        // GET: Animal
        clAnimalAcoes acAnimalAcoes = new clAnimalAcoes();
        clAnimal modAnimal = new clAnimal();
        clRacaAcoes acRacaAcoes = new clRacaAcoes();
        clRaca modRaca = new clRaca();
        clClienteAcoes acClienteAcoes = new clClienteAcoes();
        clCliente modCliente = new clCliente();

        public void carregarRaca()
        {
            List<SelectListItem> racas = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; Port= 3306; DataBase=dbVeterinario; User=root; pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("pcd_selectRaca()", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    racas.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }

            ViewBag.racas = new SelectList(racas, "Value", "Text");
        }

        public void carregarCliente()
        {
            List<SelectListItem> clientes = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; Port= 3306; DataBase=dbVeterinario; User=root; pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("pcd_selectCliente()", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clientes.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }

            ViewBag.clientes = new SelectList(clientes, "Value", "Text");
        }

        public ActionResult CadAnimal()
        {
            carregarRaca();
            carregarCliente();
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                else
                {
                    ViewBag.Nome = Session["usuarioLogado"];

                    if (Session["usuarioLogado1"] != null)
                    {
                        ViewBag.Tipo = Session["usuarioLogado1"];
                    }
                    else
                    {
                        ViewBag.Tipo = Session["usuarioLogado2"];
                    }
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CadAnimal(clAnimal Animal)
        {
            carregarRaca();
            carregarCliente();
            Animal.codRaca = Request["racas"];
            Animal.codCliente = Request["clientes"];
            acAnimalAcoes.inserirAnimal(Animal);
            ViewBag.Message = "Cadastro realizado com sucesso!";
            return RedirectToAction(nameof(ListarAnimal));
        }

        public ActionResult ListarAnimal()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("SemAcesso", "Home");
                }
                carregarRaca();
                carregarCliente();
                return View(acAnimalAcoes.buscarAnimal());
            }
        }
        public ActionResult EditarAnimal(int id)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("SemAcesso", "Home");
                }
                carregarRaca();
                carregarCliente();
                return View(acAnimalAcoes.buscarAnimal().Find(modAnimal => modAnimal.codAnimal == id));
            }
        }

        [HttpPost]
        public ActionResult EditarAnimal(clAnimal cl)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("SemAcesso", "Home");
                }
                carregarRaca();
                carregarCliente();
                cl.codRaca = Request["racas"];
                cl.codCliente = Request["clientes"];
                acAnimalAcoes.atualizarAnimal(cl);
                ViewBag.Message = "Alteração realizada com sucesso!";
                return RedirectToAction(nameof(ListarAnimal));
            }
        }
        public ActionResult ExcluirAnimal(int id)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("SemAcesso", "Home");
                }
                acAnimalAcoes.deletarAnimal(id);
                ViewBag.Message = "Exclusão realizada com sucesso!";
                return RedirectToAction(nameof(ListarAnimal));
            }
        }
    }
}
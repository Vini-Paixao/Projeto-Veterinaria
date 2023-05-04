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
    public class AtendimentoController : Controller
    {
        clAtendimentoAcoes acAtendimentoAcoes = new clAtendimentoAcoes();
        clAtendimento modAtendimento = new clAtendimento();

        public void carregarAnimal()
        {
            List<SelectListItem> animais = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; Port= 3306; DataBase=dbVeterinario; User=root; pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("pcd_selectAnimal()", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    animais.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }

            ViewBag.animais = new SelectList(animais, "Value", "Text");
        }
        public void carregarVeterinario()
        {
            List<SelectListItem> veterinarios = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; Port= 3306; DataBase=dbVeterinario; User=root; pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("pcd_selectVeterinario()", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    veterinarios.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }

            ViewBag.veterinarios = new SelectList(veterinarios, "Value", "Text");
        }

        public ActionResult CadAtendimento()
        {
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
                    carregarAnimal();
                    carregarVeterinario();
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CadAtendimento(clAtendimento Atendimento)
        {
            carregarAnimal();
            carregarVeterinario();
            Atendimento.codAnimal = Request["animais"];
            Atendimento.codVeterinario = Request["veterinarios"];
            acAtendimentoAcoes.inserirAtendimento(Atendimento);
            ViewBag.Message = "Cadastro realizado com sucesso!";
            return RedirectToAction(nameof(ListarAtendimento));
        }

        public ActionResult ListarAtendimento()
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
                return View(acAtendimentoAcoes.buscarAtendimento());
            }
        }
        public ActionResult EditarAtendimento(int id)
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
                carregarAnimal();
                carregarVeterinario();
                return View(acAtendimentoAcoes.buscarAtendimento().Find(modAtendimento => modAtendimento.codAtendimento == id));
            }
        }

        [HttpPost]
        public ActionResult EditarAtendimento(clAtendimento cl)
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
                carregarAnimal();
                carregarVeterinario();
                cl.codAnimal = Request["animais"];
                cl.codVeterinario = Request["veterinarios"];
                acAtendimentoAcoes.atualizarAtendimento(cl);
                ViewBag.Message = "Alteração realizada com sucesso!";
                return RedirectToAction(nameof(ListarAtendimento));
            }
        }
        public ActionResult ExcluirAtendimento(int id)
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
                acAtendimentoAcoes.deletarAtendimento(id);
                ViewBag.Message = "Exclusão realizada com sucesso!";
                return RedirectToAction(nameof(ListarAtendimento));
            }
        }
    }
}
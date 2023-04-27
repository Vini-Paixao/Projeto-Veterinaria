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
    public class RacaController : Controller
    {
        // GET: Raca
        clRacaAcoes acRacaAcoes = new clRacaAcoes();
        clRaca modRaca = new clRaca();
        clTipoAcoes acTipoAcoes = new clTipoAcoes();
        clTipo modTipo = new clTipo();

        public void carregarTipo()
        {
            List<SelectListItem> tipos = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; Port= 3306; DataBase=dbVeterinario; User=root; pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("pcd_selectTipo()", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tipos.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }

            ViewBag.tipos = new SelectList(tipos, "Value", "Text");
        }

        public ActionResult CadRaca()
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
                    carregarTipo();
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CadRaca(clRaca Raca)
        {
            carregarTipo();
            Raca.codTipo = Request["tipos"];
            acRacaAcoes.inserirRaca(Raca);
            ViewBag.Message = "Cadastro realizado com sucesso!";
            return RedirectToAction(nameof(ListarRaca));
        }

        public ActionResult ListarRaca()
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
                return View(acRacaAcoes.buscarRaca());
            }
        }
    }
}
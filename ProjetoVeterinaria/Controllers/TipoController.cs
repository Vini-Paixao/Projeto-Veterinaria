using ProjetoVeterinaria.Dados;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoVeterinaria.Controllers
{
    public class TipoController : Controller
    {
        // GET: Tipo
        clTipoAcoes acTipoAcoes = new clTipoAcoes();
        clTipo modTipo = new clTipo();

        public ActionResult CadTipo()
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
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CadTipo(clTipo TesteTipo)
        {
            acTipoAcoes.inserirTipo(TesteTipo);
            ViewBag.Message = "Cadastro realizado com sucesso!";
            return RedirectToAction(nameof(ListarTipo));
        }

        public ActionResult ListarTipo()
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
                return View(acTipoAcoes.buscarTipo());
            }
        }
    }
}
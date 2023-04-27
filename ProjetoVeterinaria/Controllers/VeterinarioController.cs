using ProjetoVeterinaria.Dados;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoVeterinaria.Controllers
{
    public class VeterinarioController : Controller
    {
        clVeterinarioAcoes acVeterinarioAcoes = new clVeterinarioAcoes();
        clVeterinario modVeterinario = new clVeterinario();

        // GET: Veterinario
        public ActionResult CadVeterinario()
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
        public ActionResult CadVeterinario(clVeterinario Veterinario)
        {
            acVeterinarioAcoes.inserirVeterinario(Veterinario);
            ViewBag.Message = "Cadastro realizado com sucesso!";
            return RedirectToAction(nameof(ListarVeterinario));
        }

        public ActionResult ListarVeterinario()
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
                return View(acVeterinarioAcoes.buscarVeterinario());
            }
        }
        public ActionResult EditarVeterinario(int id)
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
                return View(acVeterinarioAcoes.buscarVeterinario().Find(modVeterinario => modVeterinario.codVeterinario == id));
            }
        }

        [HttpPost]
        public ActionResult EditarVeterinario(clVeterinario cl)
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
                acVeterinarioAcoes.atualizarVeterinario(cl);
                ViewBag.Message = "Alteração realizada com sucesso!";
                return RedirectToAction(nameof(ListarVeterinario));
            }
        }
        public ActionResult excluirVeterinario(int id)
        {
            if (Session["usuariologado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipologado2"] == null)
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                acVeterinarioAcoes.deletarVeterinario(id);
                return RedirectToAction(nameof(ListarVeterinario));
            }
        }
    }
}
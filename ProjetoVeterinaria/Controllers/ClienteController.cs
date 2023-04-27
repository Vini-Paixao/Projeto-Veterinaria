using ProjetoVeterinaria.Dados;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoVeterinaria.Controllers
{
    public class ClienteController : Controller
    {
        clClienteAcoes acClienteAcoes = new clClienteAcoes();
        clCliente modCliente = new clCliente();

        // GET: Cliente
        public ActionResult CadCliente()
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
        public ActionResult CadCliente(clCliente cliente)
        {
            acClienteAcoes.inserirCliente(cliente);
            ViewBag.Message = "Cadastro realizado com sucesso!";
            return RedirectToAction(nameof(ListarCliente));
        }

        public ActionResult ListarCliente()
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
                return View(acClienteAcoes.buscarCliente());
            }
        }
        public ActionResult EditarCliente(int id)
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
                return View(acClienteAcoes.buscarCliente().Find(modCliente => modCliente.codCliente == id));
            }
        }

        [HttpPost]
        public ActionResult EditarCliente(clCliente cl)
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
                acClienteAcoes.atualizarCliente(cl);
                ViewBag.Message = "Alteração realizada com sucesso!";
                return RedirectToAction(nameof(ListarCliente));
            }
        }
        public ActionResult excluirCliente(int id)
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
                acClienteAcoes.deletarCliente(id);
                return RedirectToAction(nameof(ListarCliente));
            }
        }
    }
}
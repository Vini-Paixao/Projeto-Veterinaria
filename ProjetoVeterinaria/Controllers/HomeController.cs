using Org.BouncyCastle.Asn1.Ocsp;
using ProjetoVeterinaria.Dados;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        clVeterinarioAcoes acVeterinarioAcoes = new clVeterinarioAcoes();
        clVeterinario modVeterinario = new clVeterinario();


        acoesLogin acLg = new acoesLogin(); 
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelLogin verLogin)
        {
            acLg.TestarUsuario(verLogin);
            if (verLogin.usuario != null && verLogin.senha != null)
            {
                Session["usuarioLogado"] = verLogin.usuario.ToString();
                Session["senhaLogado"] = verLogin.senha.ToString();


                if(verLogin.tipo == "1")
                {
                    Session["tipoLogado1"] = verLogin.tipo.ToString();
                    return RedirectToAction("usuarioComum", "Home");
                }

                else
                {
                    Session["tipoLogado2"] = verLogin.tipo.ToString();
                    return RedirectToAction("usuarioAdmin", "Home");
                }
            }
            else 
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha";
                return View();
            }
        }

        public ActionResult usuarioAdmin()
        {
            if (Session["usuariologado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Nome = Session["usuariologado"];

                if (Session["usuariologado1"] != null)
                {
                    ViewBag.tipo = Session["usuariologado1"];
                }
                else
                {
                    ViewBag.tipo = Session["usuariologado2"];
                }
                return View();
            }
        }

        public ActionResult usuarioComum()
        {
            if (Session["usuariologado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Nome = Session["usuariologado"];

                if (Session["usuariologado1"] != null)
                {
                    ViewBag.tipo = Session["usuariologado1"];
                }
                else
                {
                    ViewBag.tipo = Session["usuariologado2"];
                }
                return View();
            }
        }

        public ActionResult About()
        {
            if (Session["usuariologado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Nome = Session["usuariologado"];

                if (Session["usuariologado1"] != null)
                {
                    ViewBag.tipo = Session["usuariologado1"];
                }
                else
                {
                    ViewBag.tipo = Session["usuariologado2"];
                }
                return View();
            }
        }

        public ActionResult Contact()
        {
            if (Session["usuariologado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["usuariologado2"] != null)
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                else
                {
                    ViewBag.msg = "Sua Página de Contato.";
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult semAcesso() {
            Response.Write("<script>alert('Sem acesso')</script>");
            ViewBag.message = "Você não tem acesso a essa página";
            return View();
        }

        public ActionResult BuscaVeterinario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscaVeterinario(string nome)
        {
            var listVeterinarios = acVeterinarioAcoes.buscarVeterinarioPorNome(nome);
            ViewBag.ListaVeterinarios = listVeterinarios;
            return RedirectToAction(nameof(ListarVeterinarioNome));
        }

        public ActionResult ListarVeterinarioNome()
        {
            return View();
        }


    }
}
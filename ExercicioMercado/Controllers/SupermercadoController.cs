using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExercicioMercado.Controllers
{
    public class SupermercadoController : Controller
    {
        private readonly RepositorioSupermercado repositorio;

        public SupermercadoController()
        {
            repositorio = new RepositorioSupermercado();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Supermercado> supermercados = repositorio.ObterTodos();
            ViewBag.Supermercados = supermercados;
            ViewBag.Title = "Lista de Supermercados";
            return View();
        }

        [HttpPost]
        public ActionResult Store(Supermercado supermercado)
        {
            supermercado.RegistroAtivo = true;
            int id = repositorio.Inserir(supermercado);
            return Redirect("/supermercado");
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            List<Supermercado> supermercados = new RepositorioSupermercado().ObterTodos();
            ViewBag.Supermercados = supermercados;
            return View();

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Supermercado supermercado = repositorio.ObterPeloId(id);
            ViewBag.Supermercado = supermercado;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Supermercado supermercado)
        {
            Supermercado supermercadoPrincipal = repositorio.ObterPeloId(supermercado.Id);

            supermercadoPrincipal.Cnpj = supermercado.Cnpj;
            supermercadoPrincipal.Nome = supermercado.Nome;
            supermercadoPrincipal.Faturamento = supermercado.Faturamento;

            repositorio.Alterar(supermercadoPrincipal);
            return RedirectToAction("Editar", new { id = supermercadoPrincipal.Id });
        }

    }
}
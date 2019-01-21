using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExercicioMercado.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly RepositorioFornecedor repositorio;

        public FornecedorController()
        {
            repositorio = new RepositorioFornecedor();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Fornecedor> fornecedores = repositorio.ObterTodos();
            ViewBag.Fornecedores = fornecedores;
            ViewBag.Title = "Lista de Fornecedores";
            return View();
        }

        [HttpPost]
        public ActionResult Store(Fornecedor fornecedor)
        {
            fornecedor.RegistroAtivo = true;
            int id = repositorio.Inserir(fornecedor);
            return Redirect("/fornecedor");
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            List<Fornecedor> fornecedores = new RepositorioFornecedor().ObterTodos();
            ViewBag.Fornecedores = fornecedores;
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
            Fornecedor fornecedor = repositorio.ObterPeloId(id);
            ViewBag.Fornecedor = fornecedor;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Fornecedor fornecedor)
        {
            Fornecedor fornecedorPrincipal = repositorio.ObterPeloId(fornecedor.Id);

            fornecedorPrincipal.RazaoSocial = fornecedor.RazaoSocial;
            fornecedorPrincipal.NomeFantasia = fornecedor.NomeFantasia;
            fornecedorPrincipal.InscricaoEstadual = fornecedor.InscricaoEstadual;

            repositorio.Alterar(fornecedorPrincipal);
            return RedirectToAction("Editar", new { id = fornecedorPrincipal.Id });
        }

    }
}

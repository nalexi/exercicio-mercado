using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExercicioMercado.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly RepositorioProduto repositorio;

        public ProdutoController()
        {
            repositorio = new RepositorioProduto();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Produto> produtos = repositorio.ObterTodos();
            ViewBag.Produtos = produtos;
            ViewBag.Title = "Lista de Produtos";
            return View();
        }

        [HttpPost]
        public ActionResult Store(Produto produto)
        {
            produto.RegistroAtivo = true;
            int id = repositorio.Inserir(produto);
            return Redirect("/produto");
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            List<Supermercado> supermercados = new RepositorioSupermercado().ObterTodos();
            ViewBag.Supermercados = supermercados;

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
            Produto produto = repositorio.ObterPeloId(id);
            ViewBag.Produto = produto;

            List<Supermercado> supermercados = new RepositorioSupermercado().ObterTodos();
            ViewBag.Supermercados = supermercados;

            List<Fornecedor> fornecedores = new RepositorioFornecedor().ObterTodos();
            ViewBag.Fornecedores = fornecedores;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Produto produto)
        {
            Produto produtoPrincipal = repositorio.ObterPeloId(produto.Id);
            produtoPrincipal.IdSupermercado = produto.IdSupermercado;
            produtoPrincipal.IdFornecedor = produto.IdFornecedor;
            produtoPrincipal.Nome = produto.Nome;
            produtoPrincipal.Peso = produto.Peso;
            produtoPrincipal.Preco = produto.Preco;
            repositorio.Alterar(produtoPrincipal);
            return RedirectToAction("Editar", new { id = produtoPrincipal.Id });
        }
    }
}
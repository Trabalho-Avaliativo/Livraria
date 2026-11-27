using Livraria.Models;
using Livraria.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trabalho_Avaliativo.Models;
using Trabalho_Avaliativo.Repositories;

namespace Trabalho_Avaliativo.Controllers
{
    [Authorize] // só usuário logado acessa
    public class LivroController : Controller
    {
        private readonly ILivroRepositorio _livroRepositorio;

        public LivroController(ILivroRepositorio livroRepositorio)
        {
            _livroRepositorio = livroRepositorio;
        }

        // /Livro/Index -> lista os livros
        public IActionResult Index()
        {
            var livros = _livroRepositorio.Listar();
            return View(livros);
        }

        // /Livro/Cadastrar -> mostra o formulário
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        // /Livro/Cadastrar -> recebe o formulário preenchido
        [HttpPost]
        public IActionResult Cadastrar(LivroModel livro)
        {
            if (!ModelState.IsValid)
            {
                return View(livro);
            }

            if (_livroRepositorio.IsbnExiste(livro.Isbn))
            {
                ModelState.AddModelError("Isbn", "Já existe um livro com esse ISBN");
                return View(livro);
            }

            _livroRepositorio.Inserir(livro);
            TempData["Sucesso"] = "Livro cadastrado com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
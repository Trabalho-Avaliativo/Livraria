
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trabalho_Avaliativo.Models;
using Trabalho_Avaliativo.Repositories;

namespace Trabalho_Avaliativo.Controllers
{
    public class ContaController : Controller
    {
        private readonly ILoginRepositorio _loginRepositorio;

        public ContaController(ILoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Erro = "Preencha todos os campos.";
                return View();
            }

            var usuarioExistente = await _loginRepositorio.BuscarEmail(email);

            if (usuarioExistente != null)
            {
                ViewBag.Erro = "Este e-mail já está cadastrado.";
                return View();
            }

            var novoUsuario = new LoginModel
            {
                Nome = nome,
                Email = email
            };

            var sucesso = await _loginRepositorio.Cadastrar(novoUsuario, senha);

            if (sucesso)
            {
                TempData["Sucesso"] = "Cadastro realizado com sucesso! Faça o login.";
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Erro = "Erro ao cadastrar usuário.";
            return View();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Senha))
            {
                ViewBag.Erro = "Preencha o e-mail e a senha.";
                return View(model);
            }

            var usuario = await _loginRepositorio.BuscarEmail(model.Email);

            if (usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha inválidos.";
                return View(model);
            }

            var passwordHasher = new PasswordHasher<LoginModel>();

            var resultado = passwordHasher.VerifyHashedPassword(
                usuario,
                usuario.SenhaHash,
                model.Senha
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                ViewBag.Erro = "E-mail ou senha inválidos.";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(nameof(Login));
        }
    }
}

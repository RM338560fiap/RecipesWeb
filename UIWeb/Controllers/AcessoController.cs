using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dominio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Repositorio.Context;
using UIWeb.ViewModels;

namespace UIWeb.Controllers
{
    public class AcessoController : Controller
    {
        private readonly DatabaseContext _context;

        public AcessoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: AcessoController
        public ActionResult Index(string ReturnUrl)
        {
            if(!string.IsNullOrEmpty(ReturnUrl))
                ViewBag.Message = "Realize o login para continuar!";

            var viewModel = new LoginViewModel() { vwUrlRetorno = ReturnUrl };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel loginView)
        {

            if (!ModelState.IsValid)
            {
                return View(loginView);
            }

            Login login = new Login();
            login.Senha = loginView.Hashpwd(loginView.vwSenha);
            login.Usuario = loginView.vwUsuario;

            var logins = _context.Logins.FirstOrDefault(m => m.Senha == login.Senha && m.Usuario == login.Usuario);

            if (logins == null)
            {
                ViewBag.Message = "Login Invalido";
                return View(loginView);
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, logins.Nome));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            var id = new ClaimsIdentity(claims, "password");
            var principal = new ClaimsPrincipal(id);

            HttpContext.SignInAsync("loginRecipes", principal, new AuthenticationProperties());

            if (!string.IsNullOrEmpty(loginView.vwUrlRetorno) || Url.IsLocalUrl(loginView.vwUrlRetorno))
                return Redirect(loginView.vwUrlRetorno);
            else
                return RedirectToAction("Index", "Receitas");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginViewModel loginView)
        {
            if (!ModelState.IsValid)
            {
                return View(loginView);
            }

            Login login = new Login();
            login.Nome = loginView.vwNome;
            login.Senha = loginView.Hashpwd(loginView.vwSenha);
            login.Usuario = loginView.vwUsuario;

            _context.Logins.Add(login);
            _context.SaveChanges();

            return RedirectToAction("Index", "Acesso");

        }

        public async Task<ActionResult>Sair()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}

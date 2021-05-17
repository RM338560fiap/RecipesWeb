using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositorio.Context;
using UIWeb.Models;

namespace UIWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext connContext)
        {
            _logger = logger;
            _context = connContext;
        }

        public async Task<ActionResult> Index()
        {
            var list = await _context.Receitas.Include(x=>x.Categoria).ToListAsync();

            if(list.Count==0)
                ViewBag.Message = "Lista de receitas vazia";

            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

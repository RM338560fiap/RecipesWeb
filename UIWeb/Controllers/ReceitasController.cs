using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Repositorio.Context;

namespace UIWeb.Controllers
{
    public class ReceitasController : Controller
    {
        private readonly DatabaseContext _context;

        public ReceitasController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Receitas
        public async Task<ActionResult> Index()
        {
            var receitas = await _context.Receitas.Include(x => x.Categoria).ToListAsync();
            return View(receitas);

        }

        [Authorize]
        // GET: Receitas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receitas = await _context.Receitas.Include(x=>x.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (receitas == null)
            {
                return NotFound();
            }

            return View(receitas);
        }

        [Authorize]
        // GET: Receitas/Create
        public ActionResult Create()
        {
            
            ViewBag.Categoria = ListaCategoria();
           
            return View();
        }

        [Authorize]
        // POST: Receitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Titulo,Descricao,CategoriaId")] Receitas receitas)
        {
            ViewBag.Categoria = ListaCategoria();

            if (ModelState.IsValid)
            {
                _context.Add(receitas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(receitas);
        }

        [Authorize]
        // GET: Receitas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Categoria = ListaCategoria();

            if (id == null)
            {
                return NotFound();
            }

            var receitas = await _context.Receitas.Include(x => x.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (receitas == null)
            {
                return NotFound();
            }

            return View(receitas);
        }

        [Authorize]
        // POST: Receitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,CategoriaId")] Receitas receitas)
        {
            ViewBag.Categoria = ListaCategoria();

            if (id != receitas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receitas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitasExists(receitas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(receitas);
        }

        [Authorize]
        // GET: Receitas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receitas = await _context.Receitas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (receitas == null)
            {
                return NotFound();
            }

            return View(receitas);
        }

        [Authorize]
        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var receitas = await _context.Receitas.FindAsync(id);
            _context.Receitas.Remove(receitas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceitasExists(int id)
        {
            return _context.Receitas.Any(e => e.Id == id);
        }

        public IEnumerable<SelectListItem> ListaCategoria()
        {
            IEnumerable<SelectListItem> listaCategoria =
                _context.Categorias.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString()
                });
            return listaCategoria;
        }
    }
}

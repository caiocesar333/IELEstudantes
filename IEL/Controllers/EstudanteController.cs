using IEL.Models;
using IEL.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace IEL.Controllers
{
    public class EstudanteController : Controller
    {
        private readonly EstudantesDbContext _context;

        public EstudanteController(EstudantesDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var estudantes = await _context.Estudantes.ToListAsync();
            if (estudantes == null || !estudantes.Any())
            {
                Console.WriteLine("error");
            }
            return View(estudantes);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchText) // Search method
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return View(await _context.Estudantes.ToListAsync());
            }

            var filteredEstudantes = await _context.Estudantes
                .Where(e => e.Nome.Contains(searchText))
                .ToListAsync();
            return PartialView("_EstudanteList", filteredEstudantes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Estados = Enum.GetValues(typeof(EstadoEn));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudante estudante)
        {
            if (ModelState.IsValid)
            {
                // Assume Estado is an enum and you've adjusted your database accordingly.
                // estudante.Estado will now hold the integer representation of the Estado enum value

                _context.Add(estudante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If we reach here, something went wrong
            ViewBag.Estados = Enum.GetValues(typeof(EstadoEn));
            return View(estudante);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Console.WriteLine($"Attempting to delete estudante with id: {id}");

            var estudante = await _context.Estudantes.FindAsync(id);
            if (estudante == null)
            {
                Console.WriteLine($"Estudante with id: {id} not found.");
                return NotFound();
            }

            _context.Estudantes.Remove(estudante);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Successfully deleted estudante with id: {id}");

            return RedirectToAction(nameof(Index));
        }

    }
}

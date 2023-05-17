using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP003B.FoodRecipeFinal.Data;
using COMP003B.FoodRecipeFinal.Models;

namespace COMP003B.FoodRecipeFinal.Controllers
{
    public class RecipeController : Controller
    {
        private readonly WebDevAcademyContext _context;

        public RecipeController(WebDevAcademyContext context)
        {
            _context = context;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
              return _context.Recipe != null ? 
                          View(await _context.Recipe.ToListAsync()) :
                          Problem("Entity set 'WebDevAcademyContext.Recipe'  is null.");
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipeViewModel = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeViewModel == null)
            {
                return NotFound();
            }

            return View(recipeViewModel);
        }

        // GET: Recipe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Recipe")] RecipeViewModel recipeViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeViewModel);
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipeViewModel = await _context.Recipe.FindAsync(id);
            if (recipeViewModel == null)
            {
                return NotFound();
            }
            return View(recipeViewModel);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Recipe")] RecipeViewModel recipeViewModel)
        {
            if (id != recipeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeViewModelExists(recipeViewModel.Id))
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
            return View(recipeViewModel);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipeViewModel = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeViewModel == null)
            {
                return NotFound();
            }

            return View(recipeViewModel);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipe == null)
            {
                return Problem("Entity set 'WebDevAcademyContext.Recipe'  is null.");
            }
            var recipeViewModel = await _context.Recipe.FindAsync(id);
            if (recipeViewModel != null)
            {
                _context.Recipe.Remove(recipeViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeViewModelExists(int id)
        {
          return (_context.Recipe?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

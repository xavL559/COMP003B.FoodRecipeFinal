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
    public class FoodRecipeController : Controller
    {
        private readonly WebDevAcademyContext _context;

        public FoodRecipeController(WebDevAcademyContext context)
        {
            _context = context;
        }

        // GET: FoodRecipe
        public async Task<IActionResult> Index()
        {
              return _context.FoodRecipe != null ? 
                          View(await _context.FoodRecipe.ToListAsync()) :
                          Problem("Entity set 'WebDevAcademyContext.FoodRecipe'  is null.");
        }

        // GET: FoodRecipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FoodRecipe == null)
            {
                return NotFound();
            }

            var foodRecipeViewModel = await _context.FoodRecipe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (foodRecipeViewModel == null)
            {
                return NotFound();
            }

            return View(foodRecipeViewModel);
        }

        // GET: FoodRecipe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodRecipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FoodID,RecipeID")] FoodRecipeViewModel foodRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodRecipeViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodRecipeViewModel);
        }

        // GET: FoodRecipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FoodRecipe == null)
            {
                return NotFound();
            }

            var foodRecipeViewModel = await _context.FoodRecipe.FindAsync(id);
            if (foodRecipeViewModel == null)
            {
                return NotFound();
            }
            return View(foodRecipeViewModel);
        }

        // POST: FoodRecipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FoodID,RecipeID")] FoodRecipeViewModel foodRecipeViewModel)
        {
            if (id != foodRecipeViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodRecipeViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodRecipeViewModelExists(foodRecipeViewModel.ID))
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
            return View(foodRecipeViewModel);
        }

        // GET: FoodRecipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FoodRecipe == null)
            {
                return NotFound();
            }

            var foodRecipeViewModel = await _context.FoodRecipe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (foodRecipeViewModel == null)
            {
                return NotFound();
            }

            return View(foodRecipeViewModel);
        }

        // POST: FoodRecipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FoodRecipe == null)
            {
                return Problem("Entity set 'WebDevAcademyContext.FoodRecipe'  is null.");
            }
            var foodRecipeViewModel = await _context.FoodRecipe.FindAsync(id);
            if (foodRecipeViewModel != null)
            {
                _context.FoodRecipe.Remove(foodRecipeViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodRecipeViewModelExists(int id)
        {
          return (_context.FoodRecipe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

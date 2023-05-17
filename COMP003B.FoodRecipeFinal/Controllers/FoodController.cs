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
    public class FoodController : Controller
    {
        private readonly WebDevAcademyContext _context;

        public FoodController(WebDevAcademyContext context)
        {
            _context = context;
        }

        // GET: Food
        public async Task<IActionResult> Index()
        {
              return _context.Food != null ? 
                          View(await _context.Food.ToListAsync()) :
                          Problem("Entity set 'WebDevAcademyContext.Food'  is null.");
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var foodViewModel = await _context.Food
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodViewModel == null)
            {
                return NotFound();
            }

            return View(foodViewModel);
        }

        // GET: Food/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,FoodDescription,FoodType,TimeToCook")] FoodViewModel foodViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodViewModel);
        }

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var foodViewModel = await _context.Food.FindAsync(id);
            if (foodViewModel == null)
            {
                return NotFound();
            }
            return View(foodViewModel);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,FoodDescription,FoodType,TimeToCook")] FoodViewModel foodViewModel)
        {
            if (id != foodViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodViewModelExists(foodViewModel.Id))
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
            return View(foodViewModel);
        }

        // GET: Food/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var foodViewModel = await _context.Food
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodViewModel == null)
            {
                return NotFound();
            }

            return View(foodViewModel);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Food == null)
            {
                return Problem("Entity set 'WebDevAcademyContext.Food'  is null.");
            }
            var foodViewModel = await _context.Food.FindAsync(id);
            if (foodViewModel != null)
            {
                _context.Food.Remove(foodViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodViewModelExists(int id)
        {
          return (_context.Food?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

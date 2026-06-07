using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCrudApp.Models;
using System.Linq;

namespace MvcCrudApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ
        public IActionResult Index() => View(_context.Products.ToList());

        // CREATE (GET)
        public IActionResult Create() => View();

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // UPDATE (GET)
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }

        // UPDATE (POST)
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }


       // GET: Products/Delete/5
public async Task<IActionResult> Delete(int? id)
{
    if (id == null) return NotFound();

    var product = await _context.Products
        .FirstOrDefaultAsync(m => m.Id == id);
    if (product == null) return NotFound();

    return View(product);
}

// POST: Products/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product != null)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
}

    }
}

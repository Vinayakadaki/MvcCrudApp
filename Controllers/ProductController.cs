using Microsoft.AspNetCore.Mvc;
using MvcCrudApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcCrudApp.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Product> _products = new List<Product>();

        // READ
        public IActionResult Index() => View(_products);

        // CREATE (GET)
        public IActionResult Create() => View();

        // CREATE (POST)
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
            return RedirectToAction("Index");
        }

        // UPDATE (GET)
        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        // UPDATE (POST)
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
            }
            return RedirectToAction("Index");
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);

            return RedirectToAction("Index");
        }
    }
}

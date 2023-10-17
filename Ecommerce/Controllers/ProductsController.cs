using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.BLL;
using Microsoft.CodeAnalysis;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductBLL _productBLL;

        public ProductsController(ProductBLL productBLL)
        {
            _productBLL = productBLL;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchTerm)
        {
            if(searchTerm == null)
            {
                var products = _productBLL.GetAllProducts();
                return View(products);
            }
            else
            {
                var searchResults = _productBLL.Search(searchTerm);
                return View(searchResults);
            }
        }

        [HttpGet]
        public IActionResult AddToCart(Guid productId)
        {
            _productBLL.AddToCart(productId);
            return RedirectToAction("Index"); 
        }

        public IActionResult Search()
        {
            string searchTerm = Request.Query["searchTerm"];

            return RedirectToAction("Index", new { searchTerm = searchTerm });
        }
    }
}

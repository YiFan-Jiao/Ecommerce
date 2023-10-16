using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly CartBLL _cartBLL;

        public CartController(CartBLL cartBLL)
        {
            _cartBLL = cartBLL;
        }
        public IActionResult Index()
        {
            var carts = _cartBLL.GetAllCarts().OrderBy(p => p.ProductName).ToList();
            decimal totalPrice = _cartBLL.totalPrice();
            ViewData["TotalPrice"] = totalPrice;

            var countries = _cartBLL.GetAllCountries();
            ViewBag.Countries = countries;
            return View(carts);
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            _cartBLL.RemoveFromCart(cartItemId);
            return RedirectToAction("Index");
        }
    }
}

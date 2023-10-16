using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly CartBLL _cartBLL;
        private readonly OrderBLL _orderBLL;

        public OrderController(CartBLL cartBLL, OrderBLL orderBLL)
        {
            _cartBLL = cartBLL;
            _orderBLL = orderBLL;
        }
        
        public IActionResult Index(string deliveryCountry)
        {
            ViewBag.DeliveryCountry = deliveryCountry;

            decimal totalPrice = _cartBLL.totalPrice();
            ViewData["TotalPrice"] = totalPrice;

            var convertedPriceInfo = _orderBLL.convertedPrice(totalPrice, deliveryCountry);

            ViewBag.ConversionRate = convertedPriceInfo.ConversionRate;
            ViewBag.ConvertedPrice = convertedPriceInfo.ConvertedPrice;
            ViewBag.TaxRate = convertedPriceInfo.TaxRate;
            ViewBag.TotalPriceWithTaxes = convertedPriceInfo.TotalPriceWithTaxes;

            return View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            string address = Request.Form["Address"];
            string mailingCode = Request.Form["MailingCode"];
            string deliveryCountry = Request.Form["deliveryCountry"];




            _orderBLL.CreateOrder(address, mailingCode);

            return View();
        }
    }
}

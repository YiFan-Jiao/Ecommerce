
using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class CartBLL
    {
        private readonly IRepository<Products, Guid> _productRepo;
        private readonly IRepository<Cart, int> _cartRepo;
        private readonly IRepository<Country, int> _countryRepo;
        private readonly IRepository<Order, int> _orderRepo;
        public CartBLL(IRepository<Products, Guid> productRepo, IRepository<Cart, int> cartRepo, IRepository<Country, int> countryRepo, IRepository<Order, int> orderRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _countryRepo = countryRepo;
            _orderRepo = orderRepo;
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            int orderCount = _orderRepo.GetAll().Count();

            var cartItemsWithMaxOrderID = _cartRepo.GetAll().Where(cartItem => cartItem.OrderID == orderCount + 1).ToList();

            return cartItemsWithMaxOrderID;
        }

        public void RemoveFromCart(int cartItemId)
        {
            ICollection<Products> AllProducts = _productRepo.GetAll();
            var cart = _cartRepo.Get(cartItemId);
            if (cart != null)
            {
                cart.ItemsNumInCart--;
                if (cart.ItemsNumInCart == 0)
                {
                    _cartRepo.Delete(cart);
                }
                else
                {
                    _cartRepo.Update(cart);
                }
            }
            var matchingProduct = AllProducts.FirstOrDefault(p => p.Name == cart.ProductName);
            _productRepo.Get(matchingProduct.GUID).AvailableQuantity++;
            _productRepo.Update(matchingProduct);
        }

        public decimal totalPrice()
        {
            int orderCount = _orderRepo.GetAll().Count();
            var cartItems = _cartRepo.GetAll().Where(cartItem => cartItem.OrderID == orderCount + 1).ToList();
            
            decimal totalPrice = 0m;
            //List<decimal> prices = new List<decimal>();
            foreach (var cartItem in cartItems)
            {
                var product = _productRepo.Get(_productRepo.GetAll().FirstOrDefault(p => p.Name == cartItem.ProductName).GUID);

                if (product != null)
                {
                     totalPrice += product.PriceCAD* cartItem.ItemsNumInCart;
                }
            }

            return totalPrice;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _countryRepo.GetAll();
        }

    }
}

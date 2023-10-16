
using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class CartBLL
    {
        private readonly IRepository<Products, Guid> _productRepo;
        private readonly IRepository<Cart, int> _cartRepo;
        private readonly IRepository<Country, int> _countryRepo;

        public CartBLL(IRepository<Products, Guid> productRepo, IRepository<Cart, int> cartRepo, IRepository<Country, int> countryRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _countryRepo = countryRepo;
        }

        public IEnumerable<Cart> GetAllCarts()
        {
            return _cartRepo.GetAll();
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
            var cartItems = _cartRepo.GetAll();
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

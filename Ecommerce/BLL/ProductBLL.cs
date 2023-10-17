using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.CodeAnalysis;

namespace Ecommerce.BLL
{
    public class ProductBLL
    {
        private readonly IRepository<Products,Guid> _productRepo;
        private readonly IRepository<Cart, int> _cartRepo;
        private readonly IRepository<Order, int> _orderRepo;

        public ProductBLL(IRepository<Products,Guid> productRepo, IRepository<Cart, int> cartRepo, IRepository<Order, int> orderRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
        }

        public Products CreateProduct(string name, string description, int availableQuantity, decimal priceCAD) 
        {
            Products newProduct = new Products();
            _productRepo.Create(newProduct);
            return newProduct;
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _productRepo.GetAll().OrderBy(p => p.Name).ToList();
        }

        public void AddToCart(Guid ProductId)
        {
            ICollection<Cart> Allcarts = _cartRepo.GetAll();
            var product = _productRepo.Get(ProductId);
            int orderCount = _orderRepo.GetAll().Count();
            bool exists = Allcarts.Any(cart => cart.ProductName == product.Name && cart.OrderID == orderCount+1);
            if (exists)
            {
                _cartRepo.GetAll().FirstOrDefault(cR => cR.ProductName == _productRepo.Get(ProductId).Name && cR.OrderID == orderCount + 1).ItemsNumInCart += 1;
                product.AvailableQuantity--;
                _productRepo.Update(product);
                _cartRepo.Update(_cartRepo.GetAll().FirstOrDefault(cR => cR.ProductName == _productRepo.Get(ProductId).Name));
            }
            else
            {
                Cart newCart = new Cart();
               
                newCart.OrderID = orderCount + 1;
                newCart.ProductName = _productRepo.Get(ProductId).Name;
                newCart.ItemsNumInCart = 1;
                product.AvailableQuantity--;
                _productRepo.Update(product);
                _cartRepo.Create(newCart);
            }
        }

        public ICollection<Products> Search(string searchTerm)
        {
            var products = _productRepo.GetAll().ToList(); 

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p =>
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).OrderBy(p => p.Name).ToList(); 
            }

            return products;
        }

    }
}

using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.CodeAnalysis;

namespace Ecommerce.BLL
{
    public class ProductBLL
    {
        private readonly IRepository<Products,Guid> _productRepo;
        private readonly IRepository<Cart, int> _cartRepo;

        public ProductBLL(IRepository<Products,Guid> productRepo, IRepository<Cart, int> cartRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public Products CreateProduct(string name, string description, int availableQuantity, decimal priceCAD) 
        {
            Products newProduct = new Products();
            _productRepo.Create(newProduct);
            return newProduct;
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _productRepo.GetAll();
        }

        public void AddToCart(Guid ProductId)
        {
            ICollection<Cart> Allcarts = _cartRepo.GetAll();
            var product = _productRepo.Get(ProductId);

            bool exists = Allcarts.Any(cart => cart.ProductName == product.Name);
            if (exists)
            {
                _cartRepo.GetAll().FirstOrDefault(cR => cR.ProductName == _productRepo.Get(ProductId).Name).ItemsNumInCart += 1;
                product.AvailableQuantity--;
                _productRepo.Update(product);
                _cartRepo.Update(_cartRepo.GetAll().FirstOrDefault(cR => cR.ProductName == _productRepo.Get(ProductId).Name));
            }
            else
            {
                Cart newCart = new Cart();
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
                ).ToList(); 
            }

            return products;
        }

    }
}

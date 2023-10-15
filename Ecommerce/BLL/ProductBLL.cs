using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class ProductBLL
    {
        private readonly IRepository<Products> _productRepo;
        
        public ProductBLL(IRepository<Products> productRepo)
        {
            _productRepo = productRepo;
        }

        public Products CreateProduct(string name, string description, int availableQuantity, decimal priceCAD) 
        {
            Products newProduct = new Products(
                Name = name,
                Description = description,
                AvailableQuantity = availableQuantity,
                PriceCAD = priceCAD
                );
            _productRepo.Create(newProduct);
            return newProduct;
        }

    }
}

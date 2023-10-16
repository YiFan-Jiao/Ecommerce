using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class ProductsRepo : IRepository<Products, Guid>
    {
        private readonly EcommerceContext _context;
        public ProductsRepo(EcommerceContext context)
        {
            _context = context;
        }

        public Products Create(Products entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Products entity)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public Products Get(Guid id)
        {
            Products products = _context.Products.Find(id);
            return products;
        }


        public ICollection<Products> GetAll()
        {
            return _context.Products.ToHashSet();
        }

        public Products Update(Products entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}

using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class CartRepo : IRepository<Cart>
    {
        private readonly EcommerceContext _context;
        public CartRepo(EcommerceContext context)
        {
            _context = context;
        }

        public Cart Create(Cart entity)
        {
            _context.Carts.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Cart entity)
        {
            _context.Carts.Remove(entity);
            _context.SaveChanges();
        }

        public Cart Get(int id)
        {
            Cart Cart = _context.Carts.Find(id);
            return Cart;
        }

        public ICollection<Cart> GetAll()
        {
            return _context.Carts.ToHashSet();
        }

        public Cart Update(Cart entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}

using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class OrderRepo : IRepository<Order, int>
    {
        private readonly EcommerceContext _context;
        public OrderRepo(EcommerceContext context)
        {
            _context = context;
        }

        public Order Create(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }

        public Order Get(int id)
        {
            Order Order = _context.Orders.Find(id);
            return Order;
        }

        public ICollection<Order> GetAll()
        {
            return _context.Orders.ToHashSet();
        }

        public Order Update(Order entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}

using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class CountryRepo : IRepository<Country, int>
    {
        private readonly EcommerceContext _context;
        public CountryRepo(EcommerceContext context)
        {
            _context = context;
        }

        public Country Create(Country entity)
        {
            _context.Countrys.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Country entity)
        {
            _context.Countrys.Remove(entity);
            _context.SaveChanges();
        }

        public Country Get(int id)
        {
            Country country = _context.Countrys.Find(id);
            return country;
        }

        public ICollection<Country> GetAll()
        {
            return _context.Countrys.ToHashSet();
        }

        public Country Update(Country entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}

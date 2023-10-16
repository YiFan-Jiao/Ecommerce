using Ecommerce.Data;
using Ecommerce.Models;
using System.Net;

namespace Ecommerce.BLL
{
    public class OrderBLL
    {
        private readonly IRepository<Products, Guid> _productRepo;
        private readonly IRepository<Cart, int> _cartRepo;
        private readonly IRepository<Country, int> _countryRepo;

        public OrderBLL(IRepository<Products, Guid> productRepo, IRepository<Cart, int> cartRepo, IRepository<Country, int> countryRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _countryRepo = countryRepo;
        }


        public class PriceCalculationResult
        {
            public decimal ConversionRate { get; set; }
            public decimal ConvertedPrice { get; set; }
            public decimal TaxRate { get; set; }
            public decimal TotalPriceWithTaxes { get; set; }
        }
        public PriceCalculationResult convertedPrice(decimal price,string deliveryCountry)
        {
            var country = _countryRepo.GetAll().FirstOrDefault(c => c.CountryName == deliveryCountry);
            decimal convertedPrice = country.ConversionRate * price;
            decimal totalPriceWithTaxes = (decimal)country.TaxRate * convertedPrice + convertedPrice;

            return new PriceCalculationResult
            {
                ConversionRate = country.ConversionRate,
                ConvertedPrice = convertedPrice,
                TaxRate = (decimal)country.TaxRate,
                TotalPriceWithTaxes = totalPriceWithTaxes
            };
        }

        public Order CreateOrder(string address, string mailingCode)
        {
            Order newOrder = new Order();


            return newOrder;
        }
    }
}

namespace Ecommerce.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        public decimal ConversionRate { get; set; } 
        public double TaxRate { get; set; } 

        public int? OrderId { get; set; }
        public Order Order { get; set; }

    }
}

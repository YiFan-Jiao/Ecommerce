using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Products
    {
        [Key]
        public Guid GUID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AvailableQuantity { get; set; }

        public decimal PriceCAD { get; set; }

        //public int? CartId { get; set; }

        //public Cart Cart { get; set; }
    }
}

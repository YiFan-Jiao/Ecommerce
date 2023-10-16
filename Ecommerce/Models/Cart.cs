namespace Ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
        public int ItemsNumInCart { get; set;}

        public  List<Products> Products { get; set; }

        public Order Order { get; set; }
    }
}

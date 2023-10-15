namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Country Country { get; set; }

        public string Address { get; set; }

        public string MailingCode { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }
    }
}

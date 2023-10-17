namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string DeliveryCountry { get; set; }

        public string Address { get; set; }

        public string MailingCode { get; set; }

        public decimal TotalPriceWithTaxes { get; set; }


        //public int CartId { get; set; }

        //public Cart Cart { get; set; }
    }
}

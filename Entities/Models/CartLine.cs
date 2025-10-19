namespace Entities.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; } = new();
        public decimal Quantity { get; set; }
    }
}
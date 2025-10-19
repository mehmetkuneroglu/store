namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; }
        public Cart()
        {
            Lines = new List<CartLine>();

        }
        //sepete ürün eklemek için aşağıdaki metodu kullanacağız. eğer ürün zaten yüklü ise sadece miktarı artar.
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines.Where(l=>l.Product.ProductId.Equals(product.ProductId)).FirstOrDefault();
            if(line is null)
            {
                Lines.Add(new CartLine(){
                    Product =product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity+=quantity;
            }
        }
   
        // Bir satırı silmek için aşağıdaki metodu kullanırız.
        public virtual void RemoveLine(Product product) => Lines.RemoveAll(l=>l.Product.ProductId.Equals(product.ProductId));

        // Toplam değeri almak için aşağıdaki metodu kullanırız.
        public decimal ComputeTotalValue() => Lines.Sum(p=>p.Quantity * p.Product.Price);

        // Sepeti tamamen boşaltmek için aşağıdaki metodu kullanırız.
        public virtual void ClearCart() => Lines.Clear();
        
    }

}
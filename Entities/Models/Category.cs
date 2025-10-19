namespace Entities.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        // Coolection Navigation Property
        public ICollection<Product>? Products { get; set; }

        // Veriler arasında gezinmeyi sağlamak için kullanılan bir yapı,
        // tanımlamak zorunda değiliz ancak tanımlanabilir de
    }
}
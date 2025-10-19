using Entities.Models;

namespace StoreApp.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>(); //boş referans aldırdık
        public Pagination Pagination { get; set; } = new (); //Tanımladığımız yerde newledik
        public int TotalCount =>Products.Count(); 
    }
}
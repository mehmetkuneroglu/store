using Entities.Models;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        // Bize dönecek olan ifade IQueryable<Product> olduğu için dönüş değerini bu şekilde belirttik.
        // Yine genişleteceğimiz ifade de IQueryable<Product> olduğu için genişleteceğimiz öğeyi bu şekilde belirledik.
        // Parametre olarak categoryId değerine kullanmamız gerektiği için bu değeri parametre olarak geçtik.
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products, int? categoryId)
        {
            if(categoryId is null)            
                return products;            
            else            
                return products.Where(prd => prd.CategoryId.Equals(categoryId));            
        }
        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, String? searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))            
                return products;            
            else            
                return products.Where(prd => prd.ProductName.ToLower()
                    .Contains(searchTerm.ToLower()));            
        }
        public static IQueryable<Product> FilteredByPrice(this IQueryable<Product> products, int? minPrice, int? maxPrice, bool isValidPrice)
        {
            if(isValidPrice)            
                return products.Where(prd => prd.Price>= minPrice && prd.Price<=maxPrice);            
            return products;          
        }
        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products, int pageNumber, int pageSize)
        {                      
            return products
                .Skip((pageNumber-1) * pageSize) // bize verilen pageNumber değeri 1 azaltılır. Dizi mantığı, zero basic
                .Take(pageSize); // pageSize kadar ürünü al
        }
    }
}
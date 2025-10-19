using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories
{
    public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        // RepositoryBase'den devraldığımız özelliklerle veritabanına bağlantı yapabilmek için
        // aşağıdaki constructor'ı oluşturmak zorundayız. 
        public ProductRepository(RepositoryContext context) : base(context)
        {
        }
        
        // İşlemi doğrudan base üzerindeki create metoduna gönderdik. repolarla işimiz bitti servise geçtik.
        public void CreateProduct(Product product)=> Create(product);

        public void DeleteOneProduct(Product product)=> Remove(product);   

        // GetAllProduct() çağrıldığında Base'den FindAll() çağrılması için aşağıdaki gibi tanımladık
        public IQueryable<Product> GetAllProdcts(bool tractChanges)=> FindAll(tractChanges);

        public IQueryable<Product> GetAllProdctsWithDetails(ProductRequestParameters p)
        {
            return _context.Products.FilteredByCategoryId(p.CategoryId)
                                    .FilteredBySearchTerm(p.SearchTerm)
                                    .FilteredByPrice(p.MinPrice,p.MaxPrice,p.IsValidPrice)
                                    .ToPaginate(p.PageNumber,p.PageSize);
        }

        // Interface
        public Product? GetOneProduct(int id, bool trackChanges)
        {
            return FindByCondition(p=>p.ProductId.Equals(id),trackChanges);
        }

        public IQueryable<Product> GetShowcaseProdcts(bool tractChanges)
        {
            return FindAll(tractChanges).Where(p=>p.ShowCase.Equals(true));
        }

        public void UpdateOneProduct(Product entity) => Update(entity);
        
    }

}
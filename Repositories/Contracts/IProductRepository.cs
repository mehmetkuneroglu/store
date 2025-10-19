using Entities.Models;
using Entities.RequestParameters;

namespace Repositories.Contracts
{
    public interface IProductRepository :IRepositoryBase<Product>
    {
        IQueryable<Product> GetAllProdcts(bool tractChanges);
        // Parametreli ürün listesi çağırmak için aşağıdaki kuralı tanımladık.
        IQueryable<Product> GetAllProdctsWithDetails(ProductRequestParameters p);
        IQueryable<Product> GetShowcaseProdcts(bool tractChanges);
        Product? GetOneProduct(int id, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteOneProduct(Product product);
        void UpdateOneProduct(Product entity);
    }
}
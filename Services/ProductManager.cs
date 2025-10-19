using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        //kaydetme işlemi burada verildi. 
        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
             _manager.Product.Create(product);
            _manager.Save();
        }

        public void DeleteOneProduct(int id)
        {
            var product = GetOneProduct(id,false);
            if (product is not null)
            {
                _manager.Product.DeleteOneProduct(product);
                _manager.Save();
            }
        }

        public IEnumerable<Product> GetAllProdctsWithDetails(ProductRequestParameters p)
        {
            return _manager.Product.GetAllProdctsWithDetails(p);
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProdcts(trackChanges);
        }

        public IEnumerable<Product> GetLastestProducts(int n, bool trackChanges)
        {
            return _manager.Product.GetAllProdcts(trackChanges).OrderByDescending(prd=>prd.ProductId).Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
            {
                throw new Exception("Pruduct not found");
            }
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product = GetOneProduct(id,trackChanges);
            var productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }

        public IEnumerable<Product> GetShowcaseProdcts(bool tractChanges)
        {
            return _manager.Product.GetShowcaseProdcts(tractChanges);
        }

        public void UpduteOneProduct(ProductDtoForUpdate productDto)
        {
            // var entitiy = _manager.Pruduct.GetOneProduct(productDto.ProductId, true);
            // if (entitiy is not null)
            // {
            //     entitiy.ProductName = productDto.ProductName;
            //     entitiy.Price = productDto.Price;
            //     entitiy.CategoryId = productDto.CategoryId;
            //     _manager.Save();
            // }
            var entity = _mapper.Map<Product>(productDto);
            _manager.Product.UpdateOneProduct(entity);
            _manager.Save();
        }
    }
}
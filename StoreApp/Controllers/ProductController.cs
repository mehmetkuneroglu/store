using System.Data;
using System.Security.Cryptography;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using StoreApp.Models;


namespace StoreApp.Controllers 
{
    public class ProductController : Controller
    {
        // private readonly IRepositoryManeger _maneger;

        // public ProductController(IRepositoryManeger maneger)
        // {
        //     _maneger = maneger;
        // }

        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p)
        {
            // var model = _maneger.Pruduct.GetAllProdcts(false).ToList();
            var products= _manager.ProductService.GetAllProdctsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        }
        public IActionResult Get([FromRoute(Name ="id")]int id)
        {
            // var model = _maneger.Pruduct.GetOneProduct(id, false);
            var model = _manager.ProductService.GetOneProduct(id,false);
            return View(model);
        }
        
    }
}
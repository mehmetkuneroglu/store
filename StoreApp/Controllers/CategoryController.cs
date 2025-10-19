using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class CategoryController : Controller
    {
        // private IRepositoryManeger _maneger;
        // public CategoryController(IRepositoryManeger maneger)
        // {
        //     _maneger = maneger;
        // }
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            // var model = _maneger.Category.FindAll(false);
            var model = _manager.CategoryService.GetAllCategories(false);
            return View(model);
        }
    }
}
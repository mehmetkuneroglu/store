using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")] // Her ne kadar DashboardController kısmında bu
    // ifadeyi eklemeden uygulama çalışsa da burada benzer bir ifade 
    // diğer klasör içinde olduğunda olsa gerek çalışmadı. 
    // bunu eklemek zorunda kaldık.
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var entities = _manager.ProductService.GetAllProducts(false);
            return View(entities);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesSelectList();
            return View();
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false), "CategoryId", "CategoryName", "1");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //file operation
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", file.FileName);
                //yukarıda dosyalama işlemi yapacağımız klasörün yolunu aldık. Sunucuda ana klasörün adını bilemeyeceğimiz için
                //geçerli klasörü almasını istedik ve sonra bizim kök klasörümüz ve kayıt yapacağımız klasörün adını verdik ve 
                //kullanıcıdan gelen dosyanın adını ve uzantısını almasını söyledik.


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // using ifadesinin ikinci kullanım şeklini burada görüyoruz. Bunun sebebi maliyetli sitem işlemlerinde kullanılmasıdır.
                // burada dosya yükleme işleminden sonra kullanılan kaynaklar serbest bırakılır ve sistemin rahatlaması sağlanır.
                // asenkron metod kullanıyoruz ki bu da performans açısından önemlidir. burada create işlemi yapılır. varlık kontolü 
                // yapmadık. stream ile ilgili kaynak çalışması yapabiliriz. 

                productDto.ImageUrl = String.Concat("/img/", file.FileName); //imageUrl 'i tanımlıyoruz. form içinden gelmiyor.
                _manager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Update([FromRoute] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                if (file == null)
                {
                    productDto.ImageUrl = productDto.CurrentImageUrl;
                }
                else
                {
                     //file operation
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", file.FileName);
                //yukarıda dosyalama işlemi yapacağımız klasörün yolunu aldık. Sunucuda ana klasörün adını bilemeyeceğimiz için
                //geçerli klasörü almasını istedik ve sonra bizim kök klasörümüz ve kayıt yapacağımız klasörün adını verdik ve 
                //kullanıcıdan gelen dosyanın adını ve uzantısını almasını söyledik.


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // using ifadesinin ikinci kullaım şeklini burada görüyoruz. Bununsebebi maaliyetli sitem işlemlerinde kullanılmasıdır.
                // burada dosya yükleme işleminden sonra kullanılan kaynaklar serbest bırakılır ve sstemin rahatlaması sağlanır.
                // asenkron metod kullanıyoruz ki bu da performans açısından önemlidir. burada create işlemi yapılır. varlık kontolü 
                // yapmadık. stream ile ilgili kaynak çalışması yapabiliriz. 
                    productDto.ImageUrl = String.Concat("/img/", file.FileName); //imageUrl 'i tanımlıyoruz. form içinden gelmiyor.
                }

                _manager.ProductService.UpduteOneProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete([FromRoute] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
        }
    }
}
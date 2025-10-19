using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")] //asp.net6.0 da hoca bunu kulanmak zorunda kaldı ancak 
    // biz 8.0 kullanıyoruz bunu kullanmaya gerek kalmadan uygulama çalıştı. 
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
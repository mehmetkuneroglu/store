using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public OrderController(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            _cart = cart;
        }
        // burada bir view döndürüyoruz ve model olarak Order gidiyor.
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout([FromForm] Order order)
        {
            //Eğer sepette ürün yoksa aşağıdaki mesajı döndürecek
            if(_cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            //eğer formdaki zorunlu alanlar dolu ise içerideki işlmeler yapılacak
            if(ModelState.IsValid)
            {
                //order üzerindeki satırlara sepetteki satırları ekliyoruz.
                order.Lines= _cart.Lines.ToArray();
                //Siparişleri kaydediyoruz.
                _manager.OrderService.SaveOrder(order);
                //Sepeti boşaltıyoruz.
                _cart.ClearCart();
                // Kullanıcıyı sipariş tamamlandıktan sonra gösterilecek sayfaya yönlendiriyoruz.
                // Burada OrderId değerini de sayfaya taşımış olacağız. Query String olarak
                return RedirectToPage("/Complete",new {OrderId=order.OrderId});
            }
            else
            {
                return View();
            }
        }
    }
}
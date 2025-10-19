using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;
using StoreApp.Infrastructure;

namespace StoreApp.Pages
{
    public class CartModel :PageModel
    {
        private readonly IServiceManager _manager;

        //Öncelkle Cart nesnesini ekleyeceğiz. çünkü bu nesne ile çalışacağız.
        public Cart Cart { get; set; } //IoC
        public CartModel(IServiceManager manager, Cart cartservice)
        {
            _manager = manager;       
            Cart = cartservice;    
        }


        // buraya herhangi bir url'den gelinebilir. Bunun için gelinen url'yi tutacak 
        // bir alan tanımlayacağım.
        public string ReturnUrl { get; set; } = "/";

        // burada bir metod oluşturacağız. Bu metod bu sayfa çağrıldığında yapılacak işlemleri 
        // kapsayacak. Burada returnUrl'nin bize gelmesini istiyoruz. bunun sebebi kullanıcının sepete geldiği sayfayı
        // tutmak ve kullanıcı sepete bakıp geri dönmek istediğinde geldiği sayfaya geri yönlendirebileyim.
        public void Onget(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/"; //eğer gelen değer null ise anasayfaya dönecek şekilde ata.
            // Sepetin içinde olanları da görmek için buraya eklememiz gerek ancak bunun için bir IoC servis kaydı 
            //eklememiz gerekiyor.
           // Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

        }
        // IActionResult mvc haricinde razor pages yapısında da kullanılabilir. En kapsayıcı Result yapısıdır.
        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(productId,false);
            //burada hem sayfadan gelen pruoductId değerlerini hem de returnUrl değerini tutacağız. 
            if(product is not null)
            {
                //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product,1);
                //HttpContext.Session.SetJson<Cart>("cart",Cart);
            }
            return RedirectToPage(new { returnUrl = returnUrl}); //returnUrl olarak daha sonra ayarlayacağız.
        }
        public IActionResult OnPostRemove(int id, string returnUrl)
        {
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            // Burada silme işleminde kullanacağımız yapıyı oluturuyoruz.
            Cart.RemoveLine(Cart.Lines.First(cl=>cl.Product.ProductId.Equals(id)).Product);
            // Cart içerisinde önce CartLine a ulaştık sonra product nesnesine ulaşıp metode vardik.
            //HttpContext.Session.SetJson<Cart>("cart",Cart);
            return Page();
        }

    }
}
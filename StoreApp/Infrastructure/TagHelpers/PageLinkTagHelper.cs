using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Models;

namespace StoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes ="page-model")] //div etiketi ve page-model özelliği olacak
    public class PageLinkTagHelper : TagHelper
    {
        // sayfa içinde sürekli linkler oluşturacağız. Bunun için Routing mekanizmasından gelen bir HalperFactory
        // ifadesi var, onu kulanacağız. Bu yapıyı DI yaparak sınıfımıza dahil ediyoruz.
        private readonly IUrlHelperFactory _urlHelperFactory;

        //Bu noktada proplar tanımlayarak devam edeceğiz. ilk olarak ViewContext tanımlayacağız. 
        // görünümle ilgili bir takım verileri olacak elimizde ve bunları ViewContext üzerinden organize
        // etmeye çalışacağız. inputlar gibi düşünebiliriz.
        [ViewContext] //ViewFeatures üzerinden geldi
        [HtmlAttributeNotBound] // ViewContext ifadesinin sayfayla eşleşmesini istemiyoruz. Bu yüzden kullnadık
        public ViewContext? ViewContext { get; set; } //Rendering üzerinden geldi

        // Bu noktada dışarıdan gelen bir ifademiz var o da "page-model"
        // Daha önce Pagination tanımlamıştık onuda burada kullanacağız
        public Pagination PageModel { get; set; }

        //Bir de Action ifadesi tanımlamamız gerek
        public String? PageAction { get; set; }

        // div etiketinin alacağı diğer attributelar
        public bool PageClassesEnabled { get; set; } =false;
        public string PageClass { get; set; } =string.Empty;
        public string PageClassNormal { get; set; } =string.Empty;
        public string PageClassSelected { get; set; } =string.Empty;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        // artık override işlemi yapabiliriz
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // bu bölümde sayfalama için linkler oluşturmaya çalışacağız ve bu konuda temel başvuru nesnemi ViewContext olacak
            // ViewContex MVC de bir görünüm çalıştırıldığında o görünüme ilişkin bağlamı temsil eder. Bunun içinde HttpContext, ViewData
            // TempData, Model, RouteData gibi pekçok bilgiyi tutabiliriz ve eğer buradaki bilgileri işleyeceğksek bu ifadeyi kullanabiliriz.
            // bu ViewContext ifadesini oluşturan yapı da Controller'dır. Bu yapıyı oluşturur ve görünüm nesnesine devreder.
            // ViewContext  ve PageModel ifadeleri null olmaması gerekiyor. Bu yüzden bu koşul ifadesiyle başlıyoruz.
            if(ViewContext is not null && PageModel is not null)
            {
                // Burada bir div etiketi oluşturacağız, bunun içine a etiketleri koyup link üreteceğiz ve burada actionları elde etmeye çalışacağız.
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder div = new TagBuilder("div");
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder a = new TagBuilder("a");
                    a.Attributes["href"] = urlHelper.Action(PageAction, new{ PageNumber = i});
                    if(PageClassesEnabled)
                    {
                        a.AddCssClass(PageClass);
                        a.AddCssClass(i==PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }                   
                    a.InnerHtml.Append(i.ToString());
                    div.InnerHtml.AppendHtml(a);
                }
                output.Content.AppendHtml(div.InnerHtml);
            }

        }
    }
}
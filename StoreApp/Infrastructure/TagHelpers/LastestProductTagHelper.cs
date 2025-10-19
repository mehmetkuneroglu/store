using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers
{
    [HtmlTargetElement("div", Attributes="products")]
    public class LastestProductTagHelper : TagHelper
    {
        [HtmlAttributeName("number")]
        public int Number { get; set; }

        private readonly IServiceManager _manager;

        public LastestProductTagHelper(IServiceManager manager)
        {
            _manager = manager;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // burada yeni bir Tag inşa edeceğiz bunun için TagBuilder kullanacağız.
            TagBuilder div = new TagBuilder("div"); //div tanımladık
            div.Attributes.Add("class","my-3"); // class özelliğini ekledik ve değerini verdik

            TagBuilder h6 = new TagBuilder("h6"); //h6 tanımladık
            h6.Attributes.Add("class","lead"); // class özelliğini ekledik ve değerini verdik

            TagBuilder icon = new TagBuilder("i"); // i tagı tanımladık
            icon.Attributes.Add("class","fa fa-box text-secondary"); // class özelliğini ekledik ve değerini verdik

            h6.InnerHtml.AppendHtml(icon); // iconu h6 içine aldık
            h6.InnerHtml.AppendHtml(" Lastest Products"); // h6 içine yazıyı ekledik

            TagBuilder ul = new TagBuilder("ul"); // ul tagı tanımladık
            var products = _manager.ProductService.GetLastestProducts(Number,false);

            
            foreach (Product prd in products)
            {
            
                TagBuilder li = new TagBuilder("li"); // li tagı tanımladık
                TagBuilder a = new TagBuilder("a"); // a tagı tanımladık
                a.Attributes.Add("href",$"/product/get/{prd.ProductId}"); // href değerine atama yaptık
                a.InnerHtml.AppendHtml(prd.ProductName is not null ? prd.ProductName : "");
                li.InnerHtml.AppendHtml(a); // a tagını li tagının içine koyduk
                ul.InnerHtml.AppendHtml(li); // li tagını ul tagının içine aldık
            
            }

            div.InnerHtml.AppendHtml(h6);
            div.InnerHtml.AppendHtml(ul);
            output.Content.AppendHtml(div);

        }
    }
}
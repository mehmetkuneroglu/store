using Microsoft.AspNetCore.Razor.TagHelpers; //TagHalper buradan kalıtılacak

namespace StoreApp.Infrastructure.TagHelpers
{
    // TagHalper hedefinde Html etiketleri olacağı için aşağıdaki
    // gibi bir Attribute tanımı yapacağız.
    [HtmlTargetElement("table")] // Html elementlerinden Table etiketini hedefle
    public class TableTagHelpers : TagHelper
    {
        // Burada Process adında bir metodumuz olacak ve table elementi üzerinde 
        // manipülasyon yapacağız. Bu kısımda VS'da hazır bir metod geldiğini derste
        // gördük ancak VS Code üzerinde bu metodu kendimiz oluşturacağız.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "table table-hover");
            // output değerinin özelliklerine yeni bir özellik ekle
            // ( bu class özelliği olsun, değeri de table olsun)
            // Bundan sonra eklediğimiz bütün tablolara otomatik olarak table class'ı eklenir
            // Biz bootsrap kullandığımız için boosstrap'in table özelliği tabloya yansıtılır.
        }
    }
}